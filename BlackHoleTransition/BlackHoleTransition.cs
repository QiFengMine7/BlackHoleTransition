
using MSCLoader;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using HutongGames.PlayMaker;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BlackHoleTransition
{
    [Serializable]
    [SerializeField]
    public class BlackHoleTransition : Mod
    {


        AssetBundle assetBundle;
        GameObject 跃迁核心;
        GameObject 全息;
        GameObject 跃迁核心无;
        Material m1, m2;
        GameObject 预制路, 预制路1, 预制路2, 预制路3, 预制路4;
        GameObject 碰撞箱;
        GameObject 黑洞;
        GameObject 支架;
        GameObject 购买墙, 启动, 购买墙2;
        GameObject 链接;
        Material 充电1, 充电2, 未充电, 未启动, 已启动;
        AudioSource 金币, 蓄能, 装, 装2;
        Keybind On;
        SettingsCheckBox ShowRoad;


        public override string ID => "BlackHoleTransition";
        public override string Name => "BlackHoleTransition";
        public override string Author => "MiNeDSN7";
        public override string Version => "1.0.1";
        public override string Description => "Solannum's technology";




        int c = 1;
        bool 家, 修, 商, 跑;

        float Vlocity;
        float time;
        float 大小;
        float 充电;
        public bool 开启跃迁, 安装, a;
        float 油门;
        int b;

        int 数量;

        [DllImport("user32.dll", EntryPoint = "keybd_event")]

        public static extern void Keybd_event(

    byte bvk,//虚拟键值 ESC键对应的是27

    byte bScan,//0

    int dwFlags,

    int dwExtraInfo//0

    );


        public override void ModSettings()
        {
            On = Keybind.Add(this, "On", "Startup Device", KeyCode.Alpha5);
            ShowRoad = Settings.AddCheckBox(this, "Directional Road", "Directional Road", true);
        }

        public override void OnNewGame()
        {
            string path = Path.Combine(ModLoader.GetModSettingsFolder(this), "BHT.txt");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void Load()
        {
            try
            {
                SaveData data;
                using (FileStream fsRead = new FileStream(@Path.Combine(ModLoader.GetModSettingsFolder(this), "BHT.txt"), FileMode.OpenOrCreate, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    data = (SaveData)bf.Deserialize(fsRead);
                }


                if (data != null)
                {
                    数量 = data.Number;
                    安装 = data.Instell;
                }
            }
            catch
            {

            }
        }

        public override void OnLoad()
        {




            assetBundle = LoadAssets.LoadBundle(this, "yueqianhexing.unity3d");
            跃迁核心 = assetBundle.LoadAsset("跃迁核心2") as GameObject;
            预制路 = assetBundle.LoadAsset("预制路") as GameObject;
            预制路1 = GameObject.Instantiate(this.预制路);
            预制路2 = GameObject.Instantiate(this.预制路);
            预制路3 = GameObject.Instantiate(this.预制路);
            预制路4 = GameObject.Instantiate(this.预制路);



            预制路1.SetActive(false);
            预制路2.SetActive(false);
            预制路2.transform.localEulerAngles = new Vector3(0, 334, 0);
            预制路3.SetActive(false);
            预制路3.transform.localEulerAngles = new Vector3(0, 57.00001f, 0);
            预制路4.SetActive(false);
            预制路4.transform.localEulerAngles = new Vector3(0, 99.00004f, 0);

            m1 = assetBundle.LoadAsset("m1") as Material;
            m2 = assetBundle.LoadAsset("m2") as Material;

            碰撞箱 = assetBundle.LoadAsset("碰撞箱") as GameObject;
            碰撞箱 = GameObject.Instantiate(this.碰撞箱);
            碰撞箱.transform.localPosition = new Vector3(GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.x, GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.y, GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.z);
            碰撞箱.transform.parent = GameObject.Find("SATSUMA(557kg, 248)").transform;


            跃迁核心无 = assetBundle.LoadAsset("跃迁核心2已用") as GameObject;
            支架 = assetBundle.LoadAsset("支架") as GameObject;

            GameObject.Instantiate(this.支架);
            GameObject.Find("支架(Clone)").transform.localPosition = new Vector3(GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.x, GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.y, GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.z);
            GameObject.Find("支架(Clone)").transform.parent = GameObject.Find("SATSUMA(557kg, 248)").transform;
            GameObject.Find("支架(Clone)").transform.localPosition = new Vector3(0f, 0.6f, -1.65f);
            GameObject.Find("支架(Clone)").transform.localEulerAngles = new Vector3(53.77282f, 0f, 270f);
            充电1 = assetBundle.LoadAsset("充电1") as Material;
            充电2 = assetBundle.LoadAsset("充电2") as Material;
            未充电 = assetBundle.LoadAsset("无充电") as Material;
            黑洞 = assetBundle.LoadAsset("黑洞") as GameObject;
            链接 = assetBundle.LoadAsset("链接") as GameObject;
            启动 = assetBundle.LoadAsset("启动") as GameObject;

            GameObject.Instantiate(this.启动);
            GameObject.Find("启动(Clone)").transform.localPosition = new Vector3(GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.x, GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.y, GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.z);
            GameObject.Find("启动(Clone)").transform.parent = GameObject.Find("SATSUMA(557kg, 248)").transform;
            GameObject.Find("启动(Clone)").transform.localScale = new Vector3(1, 1, 1);
            GameObject.Find("启动(Clone)").transform.localPosition = new Vector3(0, 0.75f, 0.28f);
            GameObject.Find("启动(Clone)").transform.localEulerAngles = new Vector3(90, 180, 0);
            全息 = GameObject.Find("启动(Clone)").transform.FindChild("Plane").gameObject;
            全息.transform.localScale = new Vector3(0.02120744f, 0.001870327f, 0);

            购买墙 = assetBundle.LoadAsset("购买墙") as GameObject;
            购买墙 = GameObject.Instantiate(this.购买墙);
            GameObject.Find("购买墙(Clone)").transform.localPosition = new Vector3(-1508.5f, 4f, 1242.85f);
            GameObject.Find("购买墙(Clone)").transform.localScale = new Vector3(18, 18, 18);
            GameObject.Find("购买墙(Clone)").transform.localEulerAngles = new Vector3(0, 153, 0);



            购买墙2 = assetBundle.LoadAsset("购买墙2") as GameObject;
            购买墙2 = GameObject.Instantiate(this.购买墙2);
            GameObject.Find("购买墙2(Clone)").transform.localPosition = new Vector3(-8, 0.2f, 15.1f);
            GameObject.Find("购买墙2(Clone)").transform.localScale = new Vector3(18, 18, 18);
            GameObject.Find("购买墙2(Clone)").transform.localEulerAngles = new Vector3(0, 0, 0);


            GameObject.Instantiate(this.链接);
            GameObject.Find("链接(Clone)").transform.localPosition = new Vector3(0, 0, 0);
            GameObject.Find("链接(Clone)").transform.localScale = new Vector3(0, 0, 0);

            GameObject.Instantiate(this.黑洞);
            GameObject.Find("黑洞(Clone)").transform.localPosition = new Vector3(GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.x, GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.y, GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition.z);
            GameObject.Find("黑洞(Clone)").transform.parent = GameObject.Find("SATSUMA(557kg, 248)").transform;
            GameObject.Find("黑洞(Clone)").transform.localPosition = new Vector3(0, 0.3f, 2.9f);
            GameObject.Find("黑洞(Clone)").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("黑洞(Clone)").transform.localEulerAngles = new Vector3(0, 180, 180);
            GameObject.Find("黑洞(Clone)").transform.Find("ca").transform.GetComponent<Camera>().enabled = false;

            GameObject.Find("支架(Clone)").AddComponent<Into>();
            碰撞箱.AddComponent<PengZhuang>();
            碰撞箱.SetActive(false);

            金币 = GameObject.Find("购买墙(Clone)").transform.Find("金币").gameObject.GetComponent<AudioSource>();
            蓄能 = GameObject.Find("购买墙(Clone)").transform.Find("蓄能").gameObject.GetComponent<AudioSource>();
            装 = GameObject.Find("购买墙(Clone)").transform.Find("装").gameObject.GetComponent<AudioSource>();
            装2 = GameObject.Find("购买墙(Clone)").transform.Find("装2").gameObject.GetComponent<AudioSource>();
            装2.volume = 0.2f;
            Load();

            未启动 = assetBundle.LoadAsset("New Material 11") as Material;
            已启动 = assetBundle.LoadAsset("New Material 10") as Material;

            家 = true;
            修 = false;
            商 = false;
            跑 = false;

            assetBundle.Unload(false);


        }

        bool 开始减速;
        public override void OnSave()
        {

            SaveData saveData = new SaveData();
            saveData.Number = 数量;

            if (GameObject.Find("支架(Clone)").transform.Find("跃迁核心").gameObject.transform.localScale.x == 1)
            {
                saveData.Instell = true;
            }
            else
            {
                saveData.Instell = false;
            }



            foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
            {
                if (go.gameObject.name == "Transition Core(Clone)")
                {
                    saveData.Number++;
                }
            }


            using (FileStream fsWrite = new FileStream(@Path.Combine(ModLoader.GetModSettingsFolder(this), "BHT.txt"), FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fsWrite, saveData);
            }



        }


        void Out1()
        {
            if (GameObject.Find("跃迁核心2(Clone)") == false)
            {
                GameObject.Instantiate(跃迁核心);

                this.跃迁核心.transform.localPosition = new Vector3(GameObject.Find("PLAYER").transform.localPosition.x, GameObject.Find("PLAYER").transform.localPosition.y + 1f, GameObject.Find("PLAYER").transform.localPosition.z);

                LoadAssets.MakeGameObjectPickable(this.跃迁核心);

                this.跃迁核心.name = "Transition Core";
                GameObject.Find("跃迁核心2(Clone)").transform.localScale = new Vector3(0, 0, 0);


            }
            GameObject.Instantiate(跃迁核心);

            this.跃迁核心.transform.localPosition = new Vector3(GameObject.Find("PLAYER").transform.localPosition.x, GameObject.Find("PLAYER").transform.localPosition.y + 1f, GameObject.Find("PLAYER").transform.localPosition.z);

            LoadAssets.MakeGameObjectPickable(this.跃迁核心);

            this.跃迁核心.name = "Transition Core";

        }


        void 传送()
        {
            
            if (家 == true)
            {
                GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition = new Vector3(-20, 0.2f, 27);
        

            }
            if (修 == true)
            {
             
                GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition = new Vector3(1543, 5.1f, 730);
            }
            if (商 == true)
            {
              
                GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition = new Vector3(-1555, 3.5f, 1165);
            }
            if (跑 == true)
            {
            
                GameObject.Find("SATSUMA(557kg, 248)").transform.localPosition = new Vector3(-1331, 2.7f, -935);
            }
            开始减速 = true;
            碰撞箱.SetActive(false);
        }

        bool 充能;
        float one1 = 53.77282f;
        float one2 = 0.6f;
        Vector3 速度向量;
        Vector3 速度单位向量;
        float 速度模;
        float 全息数;
        float 全息大小;
        float 全息未数 = 1;
        string 全息S;
        string 全息未S;
        float 缩放1 = 0.4746362f, 缩放2 = 0.4746362f, 缩放3 = 0.4746362f, 缩放4 = 0.4746362f;
        bool t1, t2, t3, t4;
        float roadlong;

        public override void Update()
        {
            if (a == true && ShowRoad.GetValue() == true)
            {
                if (FsmVariables.GlobalVariables.FindFsmString("PlayerCurrentVehicle").Value != "")
                {
                    if (GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.FindChild("DriverHeadPivot/CameraPivot/PivotSeatR/PLAYER"))
                    {
                        roadlong += 0.05f;

                        预制路1.transform.localScale = new Vector3(0.2034783f, 1, roadlong);
                        预制路2.transform.localScale = new Vector3(0.2034783f, 1, roadlong);
                        预制路3.transform.localScale = new Vector3(0.2034783f, 1, roadlong);
                        预制路4.transform.localScale = new Vector3(0.2034783f, 1, roadlong);

                        预制路1.gameObject.transform.localPosition = new Vector3(GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.x, GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.y - 0.2f, GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.z + 3.75f);
                        预制路2.gameObject.transform.localPosition = new Vector3(GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.x - 2f, GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.y - 0.2f, GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.z + 3.75f);
                        预制路3.gameObject.transform.localPosition = new Vector3(GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.x + 5.808f, GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.y - 0.2f, GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.z + 3.75f);
                        预制路4.gameObject.transform.localPosition = new Vector3(GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.x, GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.y - 0.2f, GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.localPosition.z - 0.1258f);

                        if (家 == true)
                        {
                            预制路1.SetActive(true);
                            预制路2.SetActive(false);
                            预制路3.SetActive(false);
                            预制路4.SetActive(false);
                        }
                        if (修 == true)
                        {

                            预制路1.SetActive(false);
                            预制路2.SetActive(true);
                            预制路3.SetActive(false);
                            预制路4.SetActive(false);

                        }
                        if (商 == true)
                        {

                            预制路1.SetActive(false);
                            预制路2.SetActive(false);
                            预制路3.SetActive(true);
                            预制路4.SetActive(false);

                        }
                        if (跑 == true)
                        {

                            预制路1.SetActive(false);
                            预制路2.SetActive(false);
                            预制路3.SetActive(false);
                            预制路4.SetActive(true);

                        }
                    }
                    else
                    {
                        预制路1.SetActive(false);
                        预制路2.SetActive(false);
                        预制路3.SetActive(false);
                        预制路4.SetActive(false);
                        roadlong = 0;
                    }
                }
                else
                {
                    预制路1.SetActive(false);
                    预制路2.SetActive(false);
                    预制路3.SetActive(false);
                    预制路4.SetActive(false);
                    roadlong = 0;
                }

            }
            else
            {
                预制路1.SetActive(false);
                预制路2.SetActive(false);
                预制路3.SetActive(false);
                预制路4.SetActive(false);
                roadlong = 0;
            }







            if (家 == true)
            {
                if (GameObject.Find("家").transform.localScale.x >= 0.3939922f)
                {
                    if (t1 == false)
                    {
                        缩放1 -= 0.005f;
                        if (缩放1 <= 0.3939922f)
                        {
                            t1 = true;
                        }
                        GameObject.Find("家").transform.localScale = new Vector3(缩放1, 缩放1, 缩放1);
                    }


                }

                if (GameObject.Find("家").transform.localScale.x <= 0.4746362f)
                {
                    if (t1 == true)
                    {
                        缩放1 += 0.005f;
                        if (缩放1 >= 0.4746362f)
                        {
                            t1 = false;
                        }
                        GameObject.Find("家").transform.localScale = new Vector3(缩放1, 缩放1, 缩放1);
                    }

                }
            }
            else
            {
                缩放1 = 0.4746362f;
                t1 = false;
                GameObject.Find("家").transform.localScale = new Vector3(缩放1, 缩放1, 缩放1);
            }



            if (修 == true)
            {
                if (GameObject.Find("修车").transform.localScale.x >= 0.3939922f)
                {
                    if (t2 == false)
                    {
                        缩放2 -= 0.005f;
                        if (缩放2 <= 0.3939922f)
                        {
                            t2 = true;
                        }
                        GameObject.Find("修车").transform.localScale = new Vector3(缩放2, 缩放2, 缩放2);
                    }


                }

                if (GameObject.Find("修车").transform.localScale.x <= 0.4746362f)
                {
                    if (t2 == true)
                    {
                        缩放2 += 0.005f;
                        if (缩放2 >= 0.4746362f)
                        {
                            t2 = false;
                        }
                        GameObject.Find("修车").transform.localScale = new Vector3(缩放2, 缩放2, 缩放2);
                    }

                }
            }
            else
            {
                缩放2 = 0.4746362f;
                t2 = false;
                GameObject.Find("修车").transform.localScale = new Vector3(缩放2, 缩放2, 缩放2);
            }



            if (商 == true)
            {
                if (GameObject.Find("商店").transform.localScale.x >= 0.3939922f)
                {
                    if (t3 == false)
                    {
                        缩放3 -= 0.005f;
                        if (缩放3 <= 0.3939922f)
                        {
                            t3 = true;
                        }
                        GameObject.Find("商店").transform.localScale = new Vector3(缩放3, 缩放3, 缩放3);
                    }


                }

                if (GameObject.Find("商店").transform.localScale.x <= 0.4746362f)
                {
                    if (t3 == true)
                    {
                        缩放3 += 0.005f;
                        if (缩放3 >= 0.4746362f)
                        {
                            t3 = false;
                        }
                        GameObject.Find("商店").transform.localScale = new Vector3(缩放3, 缩放3, 缩放3);
                    }

                }
            }
            else
            {
                缩放3 = 0.4746362f;
                t3 = false;
                GameObject.Find("商店").transform.localScale = new Vector3(缩放3, 缩放3, 缩放3);
            }




            if (跑 == true)
            {
                if (GameObject.Find("跑道").transform.localScale.x >= 0.3939922f)
                {
                    if (t4 == false)
                    {
                        缩放4 -= 0.005f;
                        if (缩放4 <= 0.3939922f)
                        {
                            t4 = true;
                        }
                        GameObject.Find("跑道").transform.localScale = new Vector3(缩放4, 缩放4, 缩放4);
                    }


                }

                if (GameObject.Find("跑道").transform.localScale.x <= 0.4746362f)
                {
                    if (t4 == true)
                    {
                        缩放4 += 0.005f;
                        if (缩放4 >= 0.4746362f)
                        {
                            t4 = false;
                        }
                        GameObject.Find("跑道").transform.localScale = new Vector3(缩放4, 缩放4, 缩放4);
                    }

                }
            }
            else
            {
                缩放4 = 0.4746362f;
                t4 = false;
                GameObject.Find("跑道").transform.localScale = new Vector3(缩放4, 缩放4, 缩放4);
            }




            Vlocity = GameObject.Find("SATSUMA(557kg, 248)").GetComponent<Drivetrain>().differentialSpeed;

            if (充能 == true)
            {
                if (one1 >= -14.5f)
                {
                    one1 -= 0.5f;
                    GameObject.Find("支架(Clone)").transform.localEulerAngles = new Vector3(one1, 0f, 270f);
                }
                if (one2 >= 0.21)
                {
                    one2 -= 0.005f;
                    GameObject.Find("支架(Clone)").transform.localPosition = new Vector3(0f, one2, -1.65f);
                }

            }
            if (充能 == false)
            {
                if (one1 <= 53.77282f)
                {
                    one1 += 0.5f;
                    GameObject.Find("支架(Clone)").transform.localEulerAngles = new Vector3(one1, 0f, 270f);
                }
                if (one2 <= 0.6)
                {
                    one2 += 0.005f;
                    GameObject.Find("支架(Clone)").transform.localPosition = new Vector3(0f, one2, -1.65f);
                }
            }
            if (FsmVariables.GlobalVariables.FindFsmString("PlayerCurrentVehicle").Value != "")
            {
                if (GameObject.Find("SATSUMA(557kg, 248)").gameObject.transform.FindChild("DriverHeadPivot/CameraPivot/PivotSeatR/PLAYER"))
                {
                    充能 = true;
                }
            }
            else
            {
                充能 = false;
            }




            if (开始减速 == true && Vlocity > 0)
            {
                速度模 = GameObject.Find("SATSUMA(557kg, 248)").gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                速度向量 = GameObject.Find("SATSUMA(557kg, 248)").gameObject.GetComponent<Rigidbody>().velocity;
                速度单位向量 = 速度向量 / 速度模;
                GameObject.Find("SATSUMA(557kg, 248)").gameObject.GetComponent<Rigidbody>().AddForce(-速度单位向量 * 557 * 50);


                if (Vlocity < 10 && 开始减速 == true)
                {
                    开始减速 = false;

                }

            }

            if (Vlocity <= 10)
            {
                if (a == true)
                {
                    if (全息大小 <= 0.01241488f)
                    {
                        全息大小 += 0.001f;
                        全息.transform.localScale = new Vector3(0.02120744f, 0.001870327f, 全息大小);
                    }
                    全息数 += 1;
                    全息未数 += 1;
                    全息S = Convert.ToString(全息数);
                    全息未S = Convert.ToString(全息未数);
                    try
                    {
                        if (全息数 > 32)
                        {
                            全息数 = 0;
                        }
                        if (全息未数 > 32)
                        {
                            全息未数 = 0;
                        }
                        全息.transform.FindChild(全息未S).gameObject.transform.GetComponent<Renderer>().material = m1;
                        全息.transform.FindChild(全息S).gameObject.transform.GetComponent<Renderer>().material = m2;

                    }
                    catch
                    {

                    }


                }
            }
            if (Vlocity > 10)
            {
                if (全息大小 >= 0)
                {
                    全息大小 -= 0.001f;
                    全息.transform.localScale = new Vector3(0.02120744f, 0.001870327f, 全息大小);
                    if (全息大小 < 0.002f)
                    {
                        全息.transform.localScale = new Vector3(0.02120744f, 0.001870327f, 0);
                    }
                    全息数 = 0;
                    全息未数 = 1;
                }

            }




            if (a == false)
            {
                if (全息大小 >= 0)
                {
                    全息大小 = 0f;
                    全息.transform.localScale = new Vector3(0.02120744f, 0.001870327f, 全息大小);

                }
                全息数 = 0;
                全息未数 = 1;
            }


            if (FsmVariables.GlobalVariables.FindFsmString("PlayerCurrentVehicle").Value != "")
            {




                if (Input.GetKeyDown(On.Key) && 安装 == true)
                {
                    a = !a;
                    if (a)
                    {
                        装.Play();
                        GameObject.Find("启动(Clone)").transform.Find("Sphere").GetComponent<Renderer>().material = 已启动;
                        开启跃迁 = true;
                    }
                    if (a == false)
                    {
                        装.Play();
                        GameObject.Find("启动(Clone)").transform.Find("Sphere").GetComponent<Renderer>().material = 未启动;
                        开启跃迁 = false;
                        time = 0;
                        大小 = 0;
                        GameObject.Find("黑洞(Clone)").transform.localScale = new Vector3(0, 0, 0);
                    }

                }

            }



            if (安装 == true)
            {
                if (b == 0)
                {
                    b = 1;
                    装.Play();
                    GameObject.Find("支架(Clone)").gameObject.transform.Find("跃迁核心").gameObject.transform.localScale = new Vector3(1, 1, 1);
                    GameObject.Find("支架(Clone)").gameObject.transform.Find("跃迁核心已用").gameObject.transform.localScale = new Vector3(0, 0, 0);
                    GameObject.Find("链接(Clone)").transform.localPosition = new Vector3(1, 0, 0);
                    装2.Play();
                }

            }

            GameObject.Find("购买墙(Clone)").gameObject.transform.Find("显示").transform.GetComponent<TextMesh>().text = Convert.ToString(数量);
            GameObject.Find("购买墙2(Clone)").gameObject.transform.Find("显示").transform.GetComponent<TextMesh>().text = Convert.ToString(数量);


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1.5f))
            {
                if (hit.collider.gameObject.name == "家")
                {
                    if (Input.GetMouseButtonDown(0) && 家 == false)
                    {
                        roadlong = 0;
                        家 = true;
                        修 = false;
                        商 = false;
                        跑 = false;

                    }

                }
                if (hit.collider.gameObject.name == "修车" && 修 == false)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        roadlong = 0;
                        家 = false;
                        修 = true;
                        商 = false;
                        跑 = false;
                    }

                }
                if (hit.collider.gameObject.name == "商店" && 商 == false)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        roadlong = 0;
                        家 = false;
                        修 = false;
                        商 = true;
                        跑 = false;
                    }

                }
                if (hit.collider.gameObject.name == "跑道" && 跑 == false)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        roadlong = 0;
                        家 = false;
                        修 = false;
                        商 = false;
                        跑 = true;
                    }

                }

                if (hit.collider.gameObject.name == "BUY")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney").Value >= 7f)
                        {
                            FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney").Value -= 7f;
                            数量++;
                            金币.Play();
                        }
                    }


                }
                if (hit.collider.gameObject.name == "OUT" || hit.collider.gameObject.name == "OUT2")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (数量 > 0)
                        {
                            数量--;
                            Out1();
                        }

                    }
                }

            }


            if (GameObject.Find("链接(Clone)").transform.localPosition.x == 1)
            {
                安装 = true;
            }
            else
            {
                安装 = false;
            }

            if (安装 == true)
            {
                充电 += 0.1f;
                if (充电 <= 1)
                {
                    GameObject.Find("支架(Clone)").transform.Find("充电壁1").GetComponent<Renderer>().material = 充电1;
                    GameObject.Find("支架(Clone)").transform.Find("充电壁2").GetComponent<Renderer>().material = 充电1;
                }
                if (充电 > 1 && 充电 <= 2)
                {
                    GameObject.Find("支架(Clone)").transform.Find("充电壁1").GetComponent<Renderer>().material = 充电2;
                    GameObject.Find("支架(Clone)").transform.Find("充电壁2").GetComponent<Renderer>().material = 充电2;
                }
                if (充电 > 2)
                {
                    充电 = 0;
                }
            }

            if (安装 == false)
            {
                充电 = 0;
                GameObject.Find("支架(Clone)").transform.Find("充电壁1").GetComponent<Renderer>().material = 未充电;
                GameObject.Find("支架(Clone)").transform.Find("充电壁2").GetComponent<Renderer>().material = 未充电;
            }



            油门 = GameObject.Find("SATSUMA(557kg, 248)").GetComponent<AxisCarController>().throttleInput;




            if (Vlocity >= 25)
            {

                if (安装 == true && 开启跃迁 == true)
                {
                    if (油门 > 0 && GameObject.Find("SATSUMA(557kg, 248)").GetComponent<AxisCarController>().handbrakeInput < 1 && GameObject.Find("SATSUMA(557kg, 248)").GetComponent<AxisCarController>().clutchInput < 0.5f)
                    {
                        碰撞箱.SetActive(true);
                        time += 0.02f;
                        if (大小 <= 2.3f)
                        {
                            GameObject.Find("黑洞(Clone)").transform.Find("ca").transform.GetComponent<Camera>().enabled = true;
                            大小 += 0.06f;
                            GameObject.Find("黑洞(Clone)").transform.localScale = new Vector3(大小, 大小, 大小);
                        }
                        if (time >= 1.5)
                        {
                            蓄能.Play();
                            GameObject.Find("链接(Clone)").transform.localPosition = new Vector3(0, 0, 0);
                      
                            GameObject.Find("支架(Clone)").transform.Find("跃迁核心").gameObject.transform.localScale = new Vector3(0, 0, 0);
                            GameObject.Find("支架(Clone)").transform.Find("跃迁核心已用").gameObject.transform.localScale = new Vector3(1, 1, 1);


                            传送();
                            开启跃迁 = false;
                            安装 = false;
                            a = false;
                            b = 0;
                            GameObject.Find("启动(Clone)").transform.Find("Sphere").GetComponent<Renderer>().material = 未启动;
                            time = 0;
                            大小 = 0;
                            GameObject.Find("黑洞(Clone)").transform.Find("ca").transform.GetComponent<Camera>().enabled = false;
                            GameObject.Find("黑洞(Clone)").transform.localScale = new Vector3(大小, 大小, 大小);

                        }
                    }
                    else
                    {
                        碰撞箱.SetActive(false);
                        time = 0;
                        if (大小 >= 0f)
                        {
                            大小 -= 0.1f;
                            GameObject.Find("黑洞(Clone)").transform.localScale = new Vector3(大小, 大小, 大小);
                            if (大小 < 0.02)
                            {
                                大小 = 0;
                                GameObject.Find("黑洞(Clone)").transform.Find("ca").transform.GetComponent<Camera>().enabled = false;
                                GameObject.Find("黑洞(Clone)").transform.localScale = new Vector3(大小, 大小, 大小);
                            }
                        }

                    }

                }

            }
            else
            {
                time = 0;
                大小 = 0;
                GameObject.Find("黑洞(Clone)").transform.Find("ca").transform.GetComponent<Camera>().enabled = false;
                GameObject.Find("黑洞(Clone)").transform.localScale = new Vector3(0, 0, 0);
            }

        }
    }
}