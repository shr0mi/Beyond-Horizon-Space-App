using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.EventSystems;

public class stargenScript : MonoBehaviour
{
    
    public GameObject player;
    public GameObject camera;
    public float starDistanceMultiplier = 5f;
    public float starSizeMin = 0f;
    public float starSizeMax = 5f;
    //private string filePath = Path.Combine(Application.streamingAssetsPath, "star_data_output.txt");
    private string filePath = Path.Combine(Application.streamingAssetsPath, "Stars/planet");
    private string prev_planet_ra="0", prev_planet_dec="0", prev_planet_parallax="0"; 
    private string planet_filePath = Path.Combine(Application.streamingAssetsPath, "planet_data_output.txt");
    private int planet_num = 1;
    public int totalPlanetNum = 10;
    public int totalStarNum = 10000;

    //Star Coords
    List<float> vx, vy, vz;
        

    //Star Color
    List<float> cr, cg, cb;
        

    //Star Radius
    List<float> rel_radius;

    //Planet data
    List<string> planet_name;
    List<string> p_ra, p_dec, p_parallax, p_ly;
    private string file_planet_datasheet = Path.Combine(Application.streamingAssetsPath, "planet_datasheet.txt");
    
    //Ui
    public GameObject ui_planet_name_text;
    public GameObject ui_planet_data;
    public GameObject ui_help;
    public GameObject ui_planet_name_input;
    private string planet_name_input;

    //Star parent
    private Transform star_gen;

    private bool isInInputState = false;

    //Skyboxes
    public Material[] skyMats;
    private int skyindex = 0;
    

    //Orthographic Mode
    private bool isInOrthoMode = false;
    private float orthoModeNum = 0;

    //Distance of projected sphere
    public float projectedSphereRadius = 1000f;
    public float radiusMultiplier = 0.5f;
    
        
    // Start is called before the first frame update
    void Start()
    {
        star_gen = gameObject.transform;
        vx = new List<float>();
        vy = new List<float>();
        vz = new List<float>();

        cr = new List<float>();
        cg = new List<float>();
        cb = new List<float>();

        rel_radius = new List<float>();

        planet_name = new List<string>();

        ReadCoordinates(filePath + "0.txt");
        ReadPlanetData(planet_filePath);

        
        GenerateStars();

        //Planet Datasheet
        p_ra = new List<string>();
        p_dec = new List<string>();
        p_parallax = new List<string>();
        p_ly = new List<string>();

        GetPlanetDatasheetData(file_planet_datasheet);

        
    }

    // Update is called once per frame
    void Update()
    {
        //Changin Planet
        if(Input.GetKeyDown(KeyCode.N) && !isInInputState && !isInOrthoMode){
            planet_num+=1;
            if(planet_num>totalPlanetNum){planet_num=1;}
            
            int temp = planet_num-1;
            if(prev_planet_ra != p_ra[temp] && prev_planet_dec != p_dec[temp] &&  prev_planet_parallax != p_parallax[temp]){
                DestroyPreviousStar();
                prev_planet_ra = p_ra[temp]; prev_planet_dec = p_dec[temp]; prev_planet_parallax = p_parallax[temp]; 
                ReadCoordinates(filePath + temp.ToString() +".txt");
                GenerateStars();
            }
            SetUiPlanetText();
            SetUiPlanetData();

            player.GetComponent<playerScript>().ChangeStarDatasheetData(planet_num);
            //Debug.Log(planet_name[planet_num-1]);
        }

        if(Input.GetKeyDown(KeyCode.H) && !isInInputState){
            if(ui_help.activeSelf){ui_help.SetActive(false);}
            else{ui_help.SetActive(true);}
        }

        if(Input.GetKeyDown(KeyCode.S) && !isInInputState && !isInOrthoMode){
            SearchPlanet();
        }

        if(isInInputState){
            GetPlanetName();
        }

        //Taking and saving Screenshot
        if(!isInInputState && Input.GetKeyDown(KeyCode.C)){
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/../" + planet_name[planet_num-1]+ "_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png", 2);
        }

        //Changing skybox
        if(Input.GetKeyDown(KeyCode.X) && !isInInputState){
            skyindex++;
            if(skyindex >= skyMats.Length){
                skyindex = 0;
            }
            RenderSettings.skybox = skyMats[skyindex];
        }

        //Ortho Mode
        //Enter Ortho Mode
        if(Input.GetKeyDown(KeyCode.O) && !isInInputState){
            if(orthoModeNum == 0 || orthoModeNum == 1){
                EnterOrthoMode();
            }else{
                ExitOrthoMode();
            }
            orthoModeNum++;
            if(orthoModeNum>=3){orthoModeNum=0;}
        }   

    }

    void EnterOrthoMode(){
        player.GetComponent<playerScript>().constellationLineSize = 2f;
        if(orthoModeNum == 0){
            player.transform.position = new Vector3(0f, -500f, 0f);
            camera.GetComponent<mouse_movement>().ChangeMinMaxLimit(1);
            foreach(Transform child in star_gen){
                if(child.position.y < 0){
                    child.gameObject.SetActive(false);
                }else{
                    //Increase Line Size
                    if(child.CompareTag("Line")){
                        child.gameObject.SetActive(true);
                        LineRenderer lr = child.GetComponent<LineRenderer>();
                        lr.startWidth = 2f;
                        lr.endWidth = 2f;
                    }

                    //Make Text Look At you
                    if(child.CompareTag("Constellation Text")){
                        child.gameObject.SetActive(true);
                        child.LookAt(player.transform.position);
                        child.Rotate(0, 180, 0);
                    }

                    child.gameObject.SetActive(true);
                    if(child.CompareTag("Star")){
                        Material material = child.GetComponent<Renderer>().material;
                        float size = material.GetFloat("_Size");
                        material.SetFloat("_Size", size * 3f);

                        child.LookAt(player.transform.position);
                        child.Rotate(0, 180, 0);
                    }
                }
                
            }
        }else{
            player.transform.position = new Vector3(0f, 500f, 0f);
            camera.GetComponent<mouse_movement>().ChangeMinMaxLimit(2);
            foreach(Transform child in star_gen){
                if(child.position.y > 0){
                    child.gameObject.SetActive(false);
                }else{
                    child.gameObject.SetActive(true);
                    if(child.CompareTag("Star")){
                        Material material = child.GetComponent<Renderer>().material;
                        float size = material.GetFloat("_Size");
                        material.SetFloat("_Size", size * 3f);

                        child.LookAt(player.transform.position);
                        child.Rotate(0, 180, 0);
                    }

                    //Increase Line Size
                    if(child.CompareTag("Line")){
                        LineRenderer lr = child.GetComponent<LineRenderer>();
                        lr.startWidth = 2f;
                        lr.endWidth = 2f;
                    }

                    //Make Text Look At you
                    if(child.CompareTag("Constellation Text")){
                        child.LookAt(player.transform.position);
                        child.Rotate(0, 180, 0);
                    }
                }
                
            }
            
        }
        isInOrthoMode = true;
    }

    void ExitOrthoMode()
    {
        player.transform.position = new Vector3(0f, 2.5f, 0f);
        camera.GetComponent<mouse_movement>().ChangeMinMaxLimit(0);
        player.GetComponent<playerScript>().constellationLineSize = 0.4f;
        foreach(Transform child in star_gen){
            child.gameObject.SetActive(true);
            if(child.CompareTag("Star")){
                Material material = child.GetComponent<Renderer>().material;
                float size = material.GetFloat("_Size");
                material.SetFloat("_Size", size / 3f);

                child.LookAt(player.transform.position);
                child.Rotate(0, 180, 0);
            }

            //Decrease Line Size
            //Increase Line Size
            if(child.CompareTag("Line")){
                LineRenderer lr = child.GetComponent<LineRenderer>();
                lr.startWidth = 0.4f;
                lr.endWidth = 0.4f;
            }

            //Make Text Look At you
            if(child.CompareTag("Constellation Text")){
                child.LookAt(player.transform.position);
                child.Rotate(0, 180, 0);
            }
        }
        isInOrthoMode = false;
    }

    void SearchPlanet()
    {
        ui_planet_name_input.SetActive(true);
        player.GetComponent<playerScript>().enabled = false;
        isInInputState = true;

        //Activates the text input field
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ui_planet_name_input);
    }

    void GetPlanetName()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            for(int i=1;i<=totalPlanetNum;i++){
                if(planet_name_input.ToLower()==planet_name[i-1].ToLower()){
                    planet_num = i;

                    int temp = planet_num-1;
                    if(prev_planet_ra != p_ra[temp] && prev_planet_dec != p_dec[temp] &&  prev_planet_parallax != p_parallax[temp]){
                        DestroyPreviousStar();
                        prev_planet_ra = p_ra[temp]; prev_planet_dec = p_dec[temp]; prev_planet_parallax = p_parallax[temp]; 
                        ReadCoordinates(filePath + temp.ToString() +".txt");
                        GenerateStars();
                    }
                    SetUiPlanetText();
                    SetUiPlanetData();

                    player.GetComponent<playerScript>().ChangeStarDatasheetData(planet_num);

                    ui_planet_name_input.SetActive(false);
                    player.GetComponent<playerScript>().enabled = true;
                    isInInputState = false;
                }
            }
            ui_planet_name_input.SetActive(false);
            player.GetComponent<playerScript>().enabled = true;
            isInInputState = false;

        }
    }

    public void GetPlanetName(string s){
        planet_name_input = s;
    }



    void ReadCoordinates(string path)
    {
        vx.Clear(); vy.Clear(); vz.Clear();
        cr.Clear(); cg.Clear(); cb.Clear();
        rel_radius.Clear();
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            // Read the file line by line
            while ((line = reader.ReadLine()) != null)
            {
                // Split the line into x, y, and z values
                string[] values = line.Split(' ');
                
                // Convert the values to float (or any required type)
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                float z = float.Parse(values[2]);

                //Get R, G, B index
                float color_r = float.Parse(values[3]);
                float color_g = float.Parse(values[4]);
                float color_b = float.Parse(values[5]);

                //Get Radius
                float radii = float.Parse(values[6]);

                // Process the x, y, z values here
                //Debug.Log($"x: {x}, y: {y}, z: {z}");
                vx.Add(x); vy.Add(z); vz.Add(y);
                cr.Add(color_r); cg.Add(color_g); cb.Add(color_b);
                rel_radius.Add(radii);
            }
        }
    }


    //Get Planet Data
    void ReadPlanetData(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            // Read the file line by line
            while ((line = reader.ReadLine()) != null)
            {
                // Split the line into x, y, and z values
                //string[] values = line.Split(' ');
                
                // Convert the values to float (or any required type)
                string pname = line;

                //Process Data
                planet_name.Add(pname);
                
            }
        }
    }

    void GetPlanetDatasheetData(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            // Read the file line by line
            while ((line = reader.ReadLine()) != null)
            {
                // Split the line into x, y, and z values
                string[] values = line.Split(' ');
                
                p_ra.Add(values[0]);
                p_dec.Add(values[1]); p_parallax.Add(values[2]);
                p_ly.Add(values[3]);
            }
        }
    }

    void GenerateStars()
    {

        //int lim = planet_num * totalStarNum; //Means We are showing 5000 stars
        //int start = lim - totalStarNum;

        for(int i=0;i<totalStarNum;i++){
            GameObject stargo = GameObject.CreatePrimitive(PrimitiveType.Quad);
            stargo.name = i.ToString();

            //Project on sphere
            float distance = Vector3.Distance (new Vector3(vx[i] * starDistanceMultiplier, vy[i] * starDistanceMultiplier, vz[i]*starDistanceMultiplier), new Vector3(0f, 0f, 0f));
            float projectionMultiplier = projectedSphereRadius/distance;

            stargo.transform.position = new Vector3(vx[i] * starDistanceMultiplier * projectionMultiplier, vy[i] * starDistanceMultiplier * projectionMultiplier, vz[i]*starDistanceMultiplier * projectionMultiplier);
            stargo.transform.LookAt(player.transform.position);
            stargo.transform.Rotate(0, 180, 0);
            //Instantiate(star, new Vector3(vx[i], vy[i], vz[i]), Quaternion.identity);

            // Get the material
            Material material = stargo.GetComponent<MeshRenderer>().material;
            
            //Assign Color
            Color baseColor = new Color(cr[i]/255f, cg[i]/255f, cb[i]/255f);
            //Color baseColor = Color.white;
            //material.color = baseColor;
            material.shader = Shader.Find("Universal Render Pipeline/Unlit/StarShader2");
            material.SetColor("_Color", baseColor);
            material.SetColor("_EmissionColor", baseColor);
            material.SetFloat("_Size", Mathf.Lerp(starSizeMin, starSizeMax, rel_radius[i] * projectionMultiplier * radiusMultiplier));

            //Set Parent
            stargo.transform.SetParent(star_gen);
            stargo.tag = "Star";
            
        }
    }

    void DestroyPreviousStar()
    {
        foreach(Transform child in star_gen)
        {
            Destroy(child.gameObject);
        }
    }

    //Set UI Planet text
    void SetUiPlanetText(){
        ui_planet_name_text.GetComponent<TMP_Text>().text = planet_name[planet_num-1];
    }

    void SetUiPlanetData()
    {
        ui_planet_data.GetComponent<TMP_Text>().text = "Ra: " + p_ra[planet_num-1] + "\n" + "Dec: " + p_dec[planet_num-1] +
        "\n" + "Parallax: " + p_parallax[planet_num-1] + "\n" + "Distance(ly): " + p_ly[planet_num-1];
    }

    

}
