using System;
using NUnit.Framework;
using Potop.Client.Editor;

namespace Potop.Client.Tests.Editor {
    public class BalanceImporterTests {
        [Test]
        public void ParseWeapons_InvalidHeader_ThrowsException() {
            string[] invalidLines = {
                "WrongHeader,AssetPath,BaseDamage,BaseFireRate,BaseProjectileSpeed,SpreadAngle,SpreadProjectileCount,LaunchAngle,AoERadius,BasePierce,KnockbackForce",
                "GuardianData,Assets/Data/Turrets/GuardianData.asset,10.0,2.0,20.0,0.0,1,0.0,0.0,0,0.0"
            };

            Assert.Throws<Exception>(() => {
                BalanceDataImporter.ParseAndValidateWeapons(invalidLines, null);
            });
        }

        [Test]
        public void ParseWeapons_InvalidValue_ThrowsException() {
            string[] invalidLines = {
                "AssetName,AssetPath,BaseDamage,BaseFireRate,BaseProjectileSpeed,SpreadAngle,SpreadProjectileCount,LaunchAngle,AoERadius,BasePierce,KnockbackForce",
                "GuardianData,Assets/Data/Turrets/GuardianData.asset,-10.0,2.0,20.0,0.0,1,0.0,0.0,0,0.0" // negative damage
            };

            Assert.Throws<Exception>(() => {
                BalanceDataImporter.ParseAndValidateWeapons(invalidLines, null);
            });
        }

        [Test]
        public void ParseWeapons_GoldenData_ParsesCorrectly() {
            string[] validLines = {
                "AssetName,AssetPath,BaseDamage,BaseFireRate,BaseProjectileSpeed,SpreadAngle,SpreadProjectileCount,LaunchAngle,AoERadius,BasePierce,KnockbackForce",
                "GuardianData,Assets/Data/Turrets/GuardianData.asset,10.0,2.0,20.0,5.0,3,45.0,1.5,2,5.0"
            };

            bool parsed = false;
            BalanceDataImporter.ParseAndValidateWeapons(validLines, (name, path, damage, fireRate, speed, spreadAngle, spreadCount, launchAngle, aoe, pierce, knockback) => {
                parsed = true;
                Assert.AreEqual("GuardianData", name);
                Assert.AreEqual("Assets/Data/Turrets/GuardianData.asset", path);
                Assert.AreEqual(10.0f, damage);
                Assert.AreEqual(2.0f, fireRate);
                Assert.AreEqual(20.0f, speed);
                Assert.AreEqual(5.0f, spreadAngle);
                Assert.AreEqual(3, spreadCount);
                Assert.AreEqual(45.0f, launchAngle);
                Assert.AreEqual(1.5f, aoe);
                Assert.AreEqual(2, pierce);
                Assert.AreEqual(5.0f, knockback);
            });

            Assert.IsTrue(parsed);
        }

        [Test]
        public void ParseEnemies_InvalidHeader_ThrowsException() {
            string[] invalidLines = {
                "WrongHeader,AssetPath,EnemyName,MaxHealth,MoveSpeed,ScoreValue,EnergyReward,BaseDamage,SpawnWeight",
                "NormalEnemy,Assets/Data/NormalEnemy.asset,Scouter,10,3.0,10,10,10,0.7"
            };

            Assert.Throws<Exception>(() => {
                BalanceDataImporter.ParseAndValidateEnemies(invalidLines, null);
            });
        }

        [Test]
        public void ParseEnemies_InvalidValue_ThrowsException() {
            string[] invalidLines = {
                "AssetName,AssetPath,EnemyName,MaxHealth,MoveSpeed,ScoreValue,EnergyReward,BaseDamage,SpawnWeight",
                "NormalEnemy,Assets/Data/NormalEnemy.asset,Scouter,-5,3.0,10,10,10,0.7" // negative health
            };

            Assert.Throws<Exception>(() => {
                BalanceDataImporter.ParseAndValidateEnemies(invalidLines, null);
            });
        }

        [Test]
        public void ParseEnemies_GoldenData_ParsesCorrectly() {
            string[] validLines = {
                "AssetName,AssetPath,EnemyName,MaxHealth,MoveSpeed,ScoreValue,EnergyReward,BaseDamage,SpawnWeight",
                "NormalEnemy,Assets/Data/NormalEnemy.asset,Scouter,10,3.0,15,20,8,0.7"
            };

            bool parsed = false;
            BalanceDataImporter.ParseAndValidateEnemies(validLines, (assetName, assetPath, enemyName, health, speed, score, energy, damage, weight) => {
                parsed = true;
                Assert.AreEqual("NormalEnemy", assetName);
                Assert.AreEqual("Assets/Data/NormalEnemy.asset", assetPath);
                Assert.AreEqual("Scouter", enemyName);
                Assert.AreEqual(10, health);
                Assert.AreEqual(3.0f, speed);
                Assert.AreEqual(15, score);
                Assert.AreEqual(20, energy);
                Assert.AreEqual(8, damage);
                Assert.AreEqual(0.7f, weight);
            });

            Assert.IsTrue(parsed);
        }
    }
}
