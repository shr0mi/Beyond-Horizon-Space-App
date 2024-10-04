using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.EventSystems;
public class playerScript : MonoBehaviour
{
    public Camera fpsCam;
    public Vector3 startpos;
    public Vector3 endpos;
    public Material lineMat;

    
    private GameObject startObj;
    private GameObject endObj;
    public GameObject selectedStar;
    private GameObject starHighlight;
    public GameObject highlightedStar;

    //Data Reading
    private string file_star_datasheet = Path.Combine(Application.streamingAssetsPath, "Star_datasheets/planet_data");
    

    List<string> s_id, s_ra, s_dec, s_parallax, s_teff, s_radius, s_lum;
    
    public GameObject ui_star_data;

    public float constellationLineSize = 0.4f;

    //Write Constellation
    public GameObject constellationName;
    private GameObject selectedConstText;
    public GameObject constInputField;

    private string input;
    private bool isInInputMode =false;

    public GameObject starGenerator;

    //Ground
    public GameObject ground;



    // Start is called before the first frame update
    void Start()
    {
        s_id = new List<string>();
        s_ra = new List<string>();
        s_dec = new List<string>();
        s_parallax = new List<string>();
        s_teff = new List<string>();
        s_radius = new List<string>();
        s_lum = new List<string>();

        
        GetStarDatasheetData(file_star_datasheet + "0.txt");
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            GetStartPos();
        }
        if(Input.GetButtonUp("Fire1")){
            GetEndPos();
        }

        if(Input.GetKeyDown(KeyCode.T) && !isInInputMode){
            WriteConstellationName();
        }

        if(Input.GetKeyDown(KeyCode.G) && !isInInputMode){
            if(ground.activeSelf == true){ground.SetActive(false);}
            else{ground.SetActive(true);}
        }

        

        WriteConstellationName2();

        MovingConstellation();

        //Quit
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }


    }

    void GetStartPos(){
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)){
            if(hit.transform.CompareTag("Star") || hit.transform.CompareTag("StarHighlighter")){
                startObj = hit.transform.gameObject;
                startpos = hit.transform.position;
                Debug.Log(hit.transform.name);
            }
        }
    }

    void GetEndPos(){
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)){
            if(startObj != null){
                if(hit.transform.CompareTag("Star") || hit.transform.CompareTag("StarHighlighter")){
                    endObj = hit.transform.gameObject;
                    endpos = hit.transform.position;

                    //If both stars are not same only then register for constellation
                    if(startObj != endObj){
                        //For creating line renderer object
                        GameObject line = new GameObject();
                        line.transform.position = startpos;
                        line.transform.SetParent(starGenerator.transform);
                        line.tag = "Line";
                        LineRenderer lr = line.AddComponent<LineRenderer>();

                        lr.startColor = Color.white;
                        lr.endColor = Color.white;
                        lr.material = lineMat;
                        lr.startWidth = constellationLineSize;
                        lr.endWidth = constellationLineSize;
                        lr.positionCount = 2;
                        lr.useWorldSpace = true;    
                                        
                        //For drawing line in the world space, provide the x,y,z values
                        lr.SetPosition(0, startpos); //x,y and z position of the starting point of the line
                        lr.SetPosition(1, endpos); //x,y and z position of the end point of the line

                        
                    }else{
                        //If both stars are same user is probably trying to get the star data
                        if(endObj.transform.CompareTag("StarHighlighter")){
                            Destroy(starHighlight);//Destroy it
                            ui_star_data.GetComponent<TMP_Text>().text = ""; //Remove Planet Data
                            Debug.Log("Destroyed");
                        }else{
                            SelectStar();
                        }
                    }
                }
                startObj = null;
            }
        }

    }

    void SelectStar(){
        highlightedStar = endObj;
        if(starHighlight != null){
            Destroy(starHighlight);
        }
        starHighlight = Instantiate(selectedStar, endObj.transform.position, Quaternion.identity);

        Debug.Log(s_id[int.Parse(highlightedStar.transform.name)]);
        WriteStarData(int.Parse(highlightedStar.transform.name));
    }

    public void ChangeStarDatasheetData(int planet_num){
        int temp = planet_num-1;
        GetStarDatasheetData(file_star_datasheet + temp.ToString() +".txt");
    }

    void GetStarDatasheetData(string path)
    {
        s_id.Clear(); s_ra.Clear(); s_dec.Clear(); s_parallax.Clear(); s_teff.Clear(); s_radius.Clear();
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            // Read the file line by line
            while ((line = reader.ReadLine()) != null)
            {
                // Split the line into x, y, and z values
                string[] values = line.Split(' ');
                
                s_id.Add(values[0]);
                s_ra.Add(values[1]); s_dec.Add(values[2]); s_parallax.Add(values[3]);
                s_teff.Add(values[4]); s_radius.Add(values[5]);
            }
        }
    }

    

    void WriteStarData(int i)
    {
        ui_star_data.GetComponent<TMP_Text>().text = "GAIA DR3 Source ID: " + s_id[i] + "\n" +
        "Ra: " + s_ra[i] + "\n" + "Dec: " + s_dec[i] + "\n" + "Parallax: " + s_parallax[i] + "\n" +
        "T eff: " + s_teff[i] + "\n" + "Radius: " + s_radius[i];
    }

    void MovingConstellation(){
        if(Input.GetMouseButton(0)){
            RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)){
                if(hit.transform.CompareTag("Constellation Text")){
                    selectedConstText = hit.transform.gameObject;
                    Debug.Log("Working");
                }
            }
        }

        if(Input.GetMouseButton(0) && selectedConstText != null){
            selectedConstText.transform.position = fpsCam.transform.position + fpsCam.transform.forward * 150f;
            selectedConstText.transform.LookAt(transform.position);
            selectedConstText.transform.Rotate(0, 180, 0);
        }

        if(Input.GetMouseButtonUp(0) && selectedConstText != null){selectedConstText=null;}
    }

    void WriteConstellationName(){
        constInputField.SetActive(true);
        starGenerator.GetComponent<stargenScript>().enabled = false; //Disabling StarGenerator to avoid 'n' key changing the planet
        isInInputMode = true;

        //Activates the text input field
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(constInputField);
    }

    void WriteConstellationName2(){
        if(isInInputMode){
            if(Input.GetKeyDown(KeyCode.Return)){
                starGenerator.GetComponent<stargenScript>().enabled = true;
                GameObject constName =  Instantiate(constellationName, fpsCam.transform.position + fpsCam.transform.forward * 150f, Quaternion.identity);
                constName.transform.SetParent(starGenerator.transform);
                constName.transform.LookAt(transform.position);
                constName.transform.Rotate(0, 180, 0);
                constName.GetComponent<TMP_Text>().text = input;
                constInputField.SetActive(false);
                isInInputMode = false;
            }
        }
    }

    public void GetConstName(string s){
        input = s;
    }


}
