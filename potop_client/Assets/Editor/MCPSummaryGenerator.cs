using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class MCPSummaryGenerator : EditorWindow
{
    // 스캔할 대상 폴더 (필요시 "Assets/Core" 등으로 수정하세요)
    private static readonly string targetFolder = "/Scripts"; 
    
    [MenuItem("Tools/Antigravity/Generate SUMMARY.md")]
    public static void GenerateSummary()
    {
        string scriptsFolderPath = Application.dataPath + targetFolder;
        // 프로젝트 최상단(Assets 폴더 밖)에 SUMMARY.md 생성
        string outputFilePath = Application.dataPath + "/../SUMMARY.md"; 

        if (!Directory.Exists(scriptsFolderPath))
        {
            Debug.LogWarning($"[MCP 요약 실패] 스캔할 폴더를 찾을 수 없습니다: {scriptsFolderPath}. 경로를 확인해 주세요.");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("# Unity Project Architecture Summary");
        sb.AppendLine("> 이 문서는 AI(MCP 서버)의 컨텍스트 파악을 돕기 위해 자동 생성된 프로젝트 구조 요약본입니다.");
        sb.AppendLine("> 세부 로직이 필요한 경우 해당 파일의 전체 내용을 요청하세요.");
        sb.AppendLine("\n---\n");

        string[] csFiles = Directory.GetFiles(scriptsFolderPath, "*.cs", SearchOption.AllDirectories);

        // 클래스, 인터페이스, 열거형, 구조체 이름을 추출하는 정규식
        Regex typeRegex = new Regex(@"\b(?:public|private|internal|protected)?\s*(?:abstract|sealed|static|partial)?\s*(class|interface|struct|enum)\s+([A-Za-z0-9_]+)", RegexOptions.Compiled);

        int fileCount = 0;

        foreach (string file in csFiles)
        {
            string relativePath = "Assets" + file.Substring(Application.dataPath.Length).Replace("\\", "/");
            string content = File.ReadAllText(file);
            
            MatchCollection matches = typeRegex.Matches(content);
            if (matches.Count > 0)
            {
                sb.AppendLine($"### 📄 `{relativePath}`");
                foreach (Match match in matches)
                {
                    string typeDef = match.Groups[1].Value;  // class, interface 등
                    string typeName = match.Groups[2].Value; // 클래스명
                    
                    // 아이콘 매핑 (시각적 구분을 위함)
                    string icon = typeDef == "interface" ? "🔗" : typeDef == "enum" ? "📋" : "📦";
                    
                    sb.AppendLine($"- {icon} **{typeName}** ({typeDef})");
                }
                sb.AppendLine();
                fileCount++;
            }
        }

        File.WriteAllText(outputFilePath, sb.ToString());
        Debug.Log($"[MCP 요약 성공] 총 {fileCount}개의 스크립트를 분석하여 SUMMARY.md를 생성했습니다. 경로: {outputFilePath}");
        
        // 에디터 갱신
        AssetDatabase.Refresh();
    }
}
