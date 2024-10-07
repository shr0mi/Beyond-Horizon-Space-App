import csv
import math

import numpy as np

#RGB conversion
def wave2rgb(temperature):
    # HOT STARS
    if temperature > 30000:
        R, G, B = 23, 41, 134
    elif temperature > 29800:
        R, G, B = 159, 191, 255
    elif temperature > 29600:
        R, G, B = 159, 191, 255
    elif temperature > 29400:
        R, G, B = 159, 191, 255
    elif temperature > 29200:
        R, G, B = 160, 191, 255
    elif temperature > 29000:
        R, G, B = 160, 191, 255
    elif temperature > 28800:
        R, G, B = 160, 191, 255
    elif temperature > 28600:
        R, G, B = 160, 191, 255
    elif temperature > 28400:
        R, G, B = 160, 191, 255
    elif temperature > 28200:
        R, G, B = 160, 191, 255
    elif temperature > 28000:
        R, G, B = 160, 191, 255
    elif temperature > 27800:
        R, G, B = 160, 192, 255
    elif temperature > 27600:
        R, G, B = 161, 192, 255
    elif temperature > 27400:
        R, G, B = 161, 192, 255
    elif temperature > 27200:
        R, G, B = 161, 192, 255
    elif temperature > 27000:
        R, G, B = 161, 192, 255
    elif temperature > 26800:
        R, G, B = 161, 192, 255
    elif temperature > 26600:
        R, G, B = 161, 192, 255
    elif temperature > 26400:
        R, G, B = 161, 192, 255
    elif temperature > 26200:
        R, G, B = 162, 192, 255
    elif temperature > 26000:
        R, G, B = 162, 192, 255
    elif temperature > 25800:
        R, G, B = 162, 193, 255
    elif temperature > 25600:
        R, G, B = 162, 193, 255
    elif temperature > 25400:
        R, G, B = 162, 193, 255
    elif temperature > 25200:
        R, G, B = 162, 193, 255
    elif temperature > 25000:
        R, G, B = 163, 193, 255
    elif temperature > 24800:
        R, G, B = 163, 193, 255
    elif temperature > 24600:
        R, G, B = 163, 193, 255
    elif temperature > 24400:
        R, G, B = 163, 193, 255
    elif temperature > 24200:
        R, G, B = 163, 193, 255
    elif temperature > 24000:
        R, G, B = 163, 194, 255
    elif temperature > 23800:
        R, G, B = 164, 194, 255
    elif temperature > 23600:
        R, G, B = 164, 194, 255
    elif temperature > 23400:
        R, G, B = 164, 194, 255
    elif temperature > 23200:
        R, G, B = 164, 194, 255
    elif temperature > 23000:
        R, G, B = 164, 194, 255
    elif temperature > 22800:
        R, G, B = 165, 194, 255
    elif temperature > 22600:
        R, G, B = 165, 195, 255
    elif temperature > 22400:
        R, G, B = 165, 195, 255
    elif temperature > 22200:
        R, G, B = 165, 195, 255
    elif temperature > 22000:
        R, G, B = 166, 195, 255
    elif temperature > 21800:
        R, G, B = 166, 195, 255
    elif temperature > 21600:
        R, G, B = 166, 195, 255
    elif temperature > 21400:
        R, G, B = 166, 195, 255
    elif temperature > 21200:
        R, G, B = 167, 196, 255
    elif temperature > 21000:
        R, G, B = 167, 196, 255
    elif temperature > 20800:
        R, G, B = 167, 196, 255
    elif temperature > 20600:
        R, G, B = 167, 196, 255
    elif temperature > 20400:
        R, G, B = 168, 196, 255
    elif temperature > 20200:
        R, G, B = 168, 197, 255
    elif temperature > 20000:
        R, G, B = 168, 197, 255
    elif temperature > 19800:
        R, G, B = 169, 197, 255
    elif temperature > 19600:
        R, G, B = 169, 197, 255
    elif temperature > 19400:
        R, G, B = 169, 197, 255
    elif temperature > 19200:
        R, G, B = 169, 198, 255
    elif temperature > 19000:
        R, G, B = 170, 198, 255
    elif temperature > 18800:
        R, G, B = 170, 198, 255
    elif temperature > 18600:
        R, G, B = 170, 198, 255
    elif temperature > 18400:
        R, G, B = 171, 198, 255
    elif temperature > 18200:
        R, G, B = 171, 199, 255
    elif temperature > 18000:
        R, G, B = 172, 199, 255
    elif temperature > 17800:
        R, G, B = 172, 199, 255
    elif temperature > 17600:
        R, G, B = 172, 199, 255
    elif temperature > 17400:
        R, G, B = 173, 200, 255
    elif temperature > 17200:
        R, G, B = 173, 200, 255
    elif temperature > 17000:
        R, G, B = 174, 200, 255
    elif temperature > 16800:
        R, G, B = 174, 201, 255
    elif temperature > 16600:
        R, G, B = 175, 201, 255
    elif temperature > 16400:
        R, G, B = 175, 201, 255
    elif temperature > 16200:
        R, G, B = 175, 201, 255
    elif temperature > 16000:
        R, G, B = 176, 202, 255
    elif temperature > 15800:
        R, G, B = 177, 202, 255
    elif temperature > 15600:
        R, G, B = 177, 202, 255
    elif temperature > 15400:
        R, G, B = 178, 203, 255
    elif temperature > 15200:
        R, G, B = 178, 203, 255
    elif temperature > 15000:
        R, G, B = 179, 204, 255
    elif temperature > 14800:
        R, G, B = 179, 204, 255
    elif temperature > 14600:
        R, G, B = 180, 204, 255
    elif temperature > 14400:
        R, G, B = 181, 205, 255
    elif temperature > 14200:
        R, G, B = 181, 205, 255
    elif temperature > 14000:
        R, G, B = 182, 206, 255
    elif temperature > 13800:
        R, G, B = 183, 206, 255
    elif temperature > 13600:
        R, G, B = 183, 207, 255
    elif temperature > 13400:
        R, G, B = 184, 207, 255
    elif temperature > 13200:
        R, G, B = 185, 208, 255
    elif temperature > 13000:
        R, G, B = 186, 208, 255
    elif temperature > 12800:
        R, G, B = 187, 209, 255
    elif temperature > 12600:
        R, G, B = 188, 209, 255
    elif temperature > 12400:
        R, G, B = 189, 210, 255
    elif temperature > 12200:
        R, G, B = 190, 210, 255
    elif temperature > 12000:
        R, G, B = 191, 211, 255
    elif temperature > 11800:
        R, G, B = 192, 212, 255
    elif temperature > 11600:
        R, G, B = 193, 212, 255
    elif temperature > 11400:
        R, G, B = 194, 213, 255
    elif temperature > 11200:
        R, G, B = 195, 214, 255
    elif temperature > 11000:
        R, G, B = 196, 215, 255
    elif temperature > 10800:
        R, G, B = 198, 216, 255
    elif temperature > 10600:
        R, G, B = 199, 216, 255
    elif temperature > 10400:
        R, G, B = 201, 217, 255
    elif temperature > 10200:
        R, G, B = 202, 218, 255
    elif temperature > 10000:
        R, G, B = 204, 219, 255
    elif temperature > 9800:
        R, G, B = 206, 220, 255
    elif temperature > 9600:
        R, G, B = 207, 221, 255
    elif temperature > 9400:
        R, G, B = 209, 223, 255
    elif temperature > 9200:
        R, G, B = 211, 224, 255
    elif temperature > 9000:
        R, G, B = 214, 225, 255
    elif temperature > 8800:
        R, G, B = 216, 227, 255
    elif temperature > 8600:
        R, G, B = 218, 228, 255
    elif temperature > 8400:
        R, G, B = 221, 230, 255
    elif temperature > 8200:
        R, G, B = 224, 231, 255
    elif temperature > 8000:
        R, G, B = 227, 233, 255
    elif temperature > 7800:
        R, G, B = 230, 235, 255
    elif temperature > 7600:
        R, G, B = 233, 237, 255
    elif temperature > 7400:
        R, G, B = 237, 239, 255
    elif temperature > 7200:
        R, G, B = 240, 241, 255
    elif temperature > 7000:
        R, G, B = 245, 243, 255
    elif temperature > 6800:
        R, G, B = 249, 246, 255
    elif temperature > 6600:
        R, G, B = 254, 249, 255
    elif temperature > 6400:
        R, G, B = 255, 248, 251
    elif temperature > 6200:
        R, G, B = 255, 245, 245
    elif temperature > 6000:
        R, G, B = 255, 243, 239
    elif temperature > 5800:
        R, G, B = 255, 240, 233
    elif temperature > 5600:
        R, G, B = 255, 238, 227
    elif temperature > 5400:
        R, G, B = 255, 235, 220
    elif temperature > 5200:
        R, G, B = 255, 232, 213
    elif temperature > 5000:
        R, G, B = 255, 228, 206
    elif temperature > 4800:
        R, G, B = 255, 225, 198
    elif temperature > 4600:
        R, G, B = 255, 221, 190
    elif temperature > 4400:
        R, G, B = 255, 217, 182
    elif temperature > 4200:
        R, G, B = 255, 213, 173
    elif temperature > 4000:
        R, G, B = 255, 209, 163
    elif temperature > 3800:
        R, G, B = 255, 204, 153
    elif temperature > 3600:
        R, G, B = 255, 199, 143
    elif temperature > 3400:
        R, G, B = 255, 193, 132
    elif temperature > 3200:
        R, G, B = 255, 187, 120
    elif temperature > 3000:
        R, G, B = 255, 180, 107
    elif temperature > 2800:
        R, G, B = 255, 173, 94
    elif temperature > 2600:
        R, G, B = 255, 165, 79
    elif temperature > 2400:
        R, G, B = 255, 157, 63
    elif temperature > 2200:
        R, G, B = 255, 147, 44
    elif temperature > 2000:
        R, G, B = 255, 137, 18
    elif temperature > 1800:
        R, G, B = 255, 126, 0
    elif temperature > 1600:
        R, G, B = 255, 115, 0
    elif temperature > 1400:
        R, G, B = 255, 101, 0
    elif temperature > 1200:
        R, G, B = 255, 83, 0
    elif temperature > 1000:
        R, G, B = 255, 56, 0
    else:
        R, G, B = 249, 18, 7  # Default value for very low temperatures

    return (int(R), int(G), int(B))


#Star data collection Code
filename = open('final_data_4.csv', 'r')
file = csv.DictReader(filename)
source_id = []
ra = []
dec = []
parallax = []
#bp_rp = []
t_eff = []
radius = []
lum_val = []

for col in file:
    source_id.append(col['source_id'])
    ra.append(col['ra'])
    dec.append(col['dec'])
    parallax.append(col['parallax'])
    t_eff.append(col['teff_gspphot'])
    radius.append(col['radius_gspphot'])
    #lum_val.append(col['lum_val'])

#Exoplanet data collection Code
filename_planet = open('PSCompPars_2024.09.18_23.05.28.csv', 'r')
file_planet = csv.DictReader(filename_planet)
ra_planet = []
dec_planet = []
parallax_planet = []
planet_name = []

for col in file_planet:
    ra_planet.append(col['ra'])
    dec_planet.append(col['dec'])
    parallax_planet.append(col['sy_dist'])
    planet_name.append(col['pl_name'])


#output files
#file_star = open('star_data_output.txt', 'w+') #output of star
#file_star_datasheet = open('star_datasheet.txt', 'w+')
file_planet = open('planet_data_output.txt', 'w+')  # output of star
file_planet_datasheet = open('planet_datasheet.txt', 'w+')

coordMultiplier = 20

prev_list = []
prev_x = float(0)
prev_y = float(0)
prev_z = float(0)


#Looping Over All planets
for i in range(0, 100):
    print(planet_name[i])
    #print(planet_name[i] + " " + ra_planet[i] + " " + dec_planet[i] + " " + parallax_planet[i])
    #Calculating Planet Coords
    phi_p = np.deg2rad(90.0 - float(dec_planet[i]))
    theta_p = np.deg2rad(float(ra_planet[i]))
    p_p = float(parallax_planet[i])

    x_p = p_p * np.sin(phi_p) * np.cos(theta_p) * coordMultiplier
    y_p = p_p * np.sin(phi_p) * np.sin(theta_p) * coordMultiplier
    z_p = p_p * np.cos(phi_p) * coordMultiplier

    print(str(x_p) + " " + str(y_p) + " " + str(z_p) + "\n")

    #A list that contains (distance, index)
    dis_list = []
    sorted_dis_list = []

    #Write Planet Info
    file_planet.write(planet_name[i] + "\n")
    file_planet_datasheet.write(str(ra_planet[i]) + " " + str(dec_planet[i]) + " " + str(parallax_planet[i]) + " " + str(float(parallax_planet[i]) * 3.26) + "\n")

    if (prev_x, prev_y, prev_z) != (x_p, y_p, z_p):
        prev_x, prev_y, prev_z = x_p, y_p, z_p
        for j in range(len(ra)):
            # Calculating Star Coords
            phi = np.deg2rad(90.0 - float(dec[j]))
            theta = np.deg2rad(float(ra[j]))
            p = 1000/float(parallax[j]) #mas to parsec

            x = p * np.sin(phi) * np.cos(theta) * coordMultiplier
            y = p * np.sin(phi) * np.sin(theta) * coordMultiplier
            z = p * np.cos(phi) * coordMultiplier

            distance = math.sqrt(pow((x_p - x),2) + pow((y_p - y),2) + pow((z_p - z),2))
            dis_list.append((distance, j))

        #Sort the list by distance, now the first 10k of this list are the closest 10k
        sorted_dis_list = sorted(dis_list, key=lambda x:x[0])
        prev_list = sorted_dis_list
    else:
        sorted_dis_list = prev_list

    #Iterating over sorted list
    count = 1
    limit = 20000

    filepath1 = "Data/Stars" + "/planet" + str(i) + ".txt"
    filepath2 = "Data/Star_datasheets" + "/planet_data" + str(i) + ".txt"
    file_star = open(filepath1, 'w+')  # output of star
    file_star_datasheet = open(filepath2, 'w+')

    for j in range(len(sorted_dis_list)):
        index = sorted_dis_list[j][1] #index stores star's index in 1M data

        # Calculating Coords
        phi = np.deg2rad(90.0 - float(dec[index]))
        theta = np.deg2rad(float(ra[index]))
        p = 1000/float(parallax[index])

        #Origin Shifting
        X = p * np.sin(phi) * np.cos(theta) * coordMultiplier - x_p
        Y = p * np.sin(phi) * np.sin(theta) * coordMultiplier - y_p
        Z = p * np.cos(phi) * coordMultiplier - z_p

        #print(str(sorted_dis_list[j][0]) + " " + str(X) + " " + str(Y) + " " + str(Z))
        if(math.sqrt(X*X + Y*Y + Z*Z) < 1):
            continue
        #print(str(math.sqrt(X*X + Y*Y + Z*Z)))

        # Calculating Wavelength and Color
        if t_eff[index] == "":
            T=round(np.random.uniform(2000, 8000))
        else:
            T = float(t_eff[index])
        color = wave2rgb(T)
        # Wiens law
        #wavelength = (0.0029 / T) * 1000000000
        #color = wave2rgb(wavelength)

        # Radius
        if radius[index] == "":
            r=round(np.random.uniform(0.2, 0.8), 2)
        else:
            r = float(radius[index])


        #Star Output write
        file_star.write(str(X) + " " + str(Y) + " " + str(Z) + " " + str(color[0]) + " " + str(color[1]) + " " + str(
            color[2]) + " " + str(r) + "\n")
        file_star_datasheet.write(str(source_id[index]) + " " + str(ra[index]) + " " + str(dec[index]) + " " + str(p) + " " + str(T) + " " + str(r) + " " + "\n")

        count += 1
        if count > limit:
            break












