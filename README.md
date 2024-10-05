# Beyond-Horizon-Space-App

Beyond horizon is an app we have developed that addresses the issue posed by the difficulty in simulating the skies of planets other than the Earth, namely the exoplanets. Our app meticulously combines data from GAIA DR3 and NASA Exoplanet Archive to efficiently simulate the skies of exoplanets. Our app also features high-quality image exportation and constellation drawing facilities. The app is capable of visualizing night sky with 20k acurate star positions from 5500+ planets. 

<img src = "https://github.com/shr0mi/Beyond-Horizon-Space-App/blob/main/readme-image.png">

## Documentation

You can find all the technical details of the project here: [Dowload PDF](https://drive.google.com/file/d/1IU9bx-2ejSo0HHmrbVRZHzcxvkcIpqw0/view?usp=sharing)

## Builds

You can download the app from here:

v1.07: 20k stars around each planet + star color correction: [Download v1.07](https://drive.google.com/file/d/1mn850jtW48LBSUc3E1c5f21xL1L-uVYx/view?usp=sharing)

v1.06: 10k stars around each planet: [Download v1.06](https://drive.google.com/file/d/1ENZFVHz--x8HAr5L1uj61iJ0u6_FMxb5/view?usp=sharing)

Instructions: extract the .zip file and open the app using the .exe file

## Note for developer

The "Data Analysis" folder contains the python script that writes data in different .txt files. In order for changing data one must paste those files inside "Assets/StreamingAssests/". The python script also uses a .csv file which you can acquire using the ADQL query from documentation.   
