using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace RPG_Game.GameResource
{
    class GameRes
    {
        public static List<Hero> heroList = new List<Hero>();
        public static List<Map> mapList = new List<Map>();
        public static List<Medicine> mediList = new List<Medicine>();
        public static List<Monster> mobList = new List<Monster>();
        public static List<Weapon> weaponList = new List<Weapon>();

        public static void LoadGameRes()
        {
            LoadXmlData();

        }
        public static void LoadXmlData()
        {
            const string xmlPath = "../../XMLFile1.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            XmlElement root = doc.DocumentElement;//获取根节点
            LoadHeroData(root);
            LoadMapData(root);
            LoadMedicineData(root);
            LoadMonsterData(root);
            LoadWeaponData(root);
            foreach (var item in mapList)
            {
                Console.WriteLine(item.Name);
            }
            
        }

        static void LoadHeroData(XmlElement root)
        {
            XmlElement Library = root["hero_library"];
            XmlNodeList List = Library.ChildNodes;
            foreach (XmlNode node in List)//每一个二级节点
            {
                Hero myHero = new Hero();
                myHero.ID = Convert.ToInt32(node.Attributes["id"].Value);
                myHero.Class = node.SelectSingleNode("class").InnerText;
                myHero.HP = Convert.ToInt32(node.SelectSingleNode("hp").InnerText);
                myHero.Skill = node.SelectSingleNode("skill").InnerText;
                myHero.Attack = Convert.ToInt32(node.SelectSingleNode("attack").InnerText);
                myHero.Tips = node.SelectSingleNode("tips").InnerText ;
                myHero.Percent = Convert.ToDouble(node.SelectSingleNode("percent").InnerText);
                heroList.Add(myHero);
            }
        }
        private static void LoadWeaponData(XmlElement root)
        {
            XmlElement Library = root["weapon_library"];
            XmlNodeList List = Library.ChildNodes;
            foreach (XmlNode node in List)//每一个二级节点
            {
                Weapon myWeapon = new Weapon();
                myWeapon.ID = Convert.ToInt32(node.Attributes["id"].Value);
                myWeapon.Name = node.SelectSingleNode("name").InnerText;
                myWeapon.Price= Convert.ToInt32(node.SelectSingleNode("price").InnerText);
                myWeapon.Attack = Convert.ToInt32(node.SelectSingleNode("attack").InnerText);
                weaponList.Add(myWeapon);
            }
        }

        private static void LoadMonsterData(XmlElement root)
        {
            XmlElement Library = root["monster_library"];
            XmlNodeList List = Library.ChildNodes;
            foreach (XmlNode node in List)//每一个二级节点
            {
                Monster myMonster = new Monster();
                myMonster.ID = Convert.ToInt32(node.Attributes["id"].Value);
                myMonster.Name = node.SelectSingleNode("name").InnerText;
                myMonster.HP = Convert.ToInt32(node.SelectSingleNode("hp").InnerText);
                myMonster.Attack = Convert.ToInt32(node.SelectSingleNode("attack").InnerText);
                myMonster.Gold = Convert.ToInt32(node.SelectSingleNode("gold").InnerText);
                mobList.Add(myMonster);
            }
        }

        private static void LoadMedicineData(XmlElement root)
        {
            XmlElement Library = root["medicine_library"];
            XmlNodeList List = Library.ChildNodes;
            foreach (XmlNode node in List)//每一个二级节点
            {
                Medicine myMedicine = new Medicine();
                myMedicine.ID = Convert.ToInt32(node.Attributes["id"].Value);
                myMedicine.Name = node.SelectSingleNode("name").InnerText;
                myMedicine.RecoveredHP = Convert.ToInt32(node.SelectSingleNode("hp").InnerText);
                myMedicine.Price = Convert.ToInt32(node.SelectSingleNode("price").InnerText);
                mediList.Add(myMedicine);
            }
        }

        private static void LoadMapData(XmlElement root)
        {
            XmlElement Library = root["map_library"];
            XmlNodeList List = Library.ChildNodes;
            foreach (XmlNode node in List)//每一个二级节点
            {
                Map myMap = new Map();
                myMap.ID = Convert.ToInt32(node.Attributes["id"].Value);
                myMap.Name = node.SelectSingleNode("name").InnerText;
                myMap.MonsterLocation = StringToArray(node.SelectSingleNode("monster").InnerText);
                myMap.TreasureLocation = StringToArray(node.SelectSingleNode("treasure").InnerText);
                mapList.Add(myMap);
            }
        }
        private static int[] StringToArray(string str)
        {
            string[] strArray = str.Split(',');
            int[] intArray = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                intArray[i] = Convert.ToInt32(strArray[i]);
            }
            return intArray;
        }



    }
}

