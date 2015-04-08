using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;

using Component;

namespace Data
{
    class Parameters
    {
        private double[] cargo;			// Carga transportada: navio x matéria-prima

        private int[] iaux;

        private int numShips;		// Número de navios
        private int numTidals;		// Número de marés
        private int numBerths;		// Número de berços
        private int numMaterials;	// Número de matérias


        public Ship[] ship;
        public Berth[] berth;
        public Material[] material;

        public Parameters(string file)
        {
            readData(file);
        }
 

        private void readData(string file){
            string[] lines = System.IO.File.ReadAllLines(file);


            string[] separators = {"set", "N", " ", ":=", ";", "M", "K", "L"};
            string[] separators_materials = { " ", ":=", ";", "\t"};

            int p = 0;

            string[] split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //Número de Navios
            while ((Convert.ToString(lines[p][0]) + Convert.ToString(lines[p][1]) + Convert.ToString(lines[p][2])) != "set")
            {
                p++;
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            iaux = new int[split.Length];
            for(int n = 0; n < split.Length; n++){
                iaux[n] = Convert.ToInt32(split[n]);
            }
            numShips = iaux.Length;
            ship = new Ship[numShips];
            for (int n = 0; n < ship.Length; n++)
            {
                ship[n] = new Ship();
            }
            p++;

            //Número de Marés 
            while ((Convert.ToString(lines[p][0]) + Convert.ToString(lines[p][1]) + Convert.ToString(lines[p][2])) != "set")
            {
                p++;
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            iaux = new int[split.Length];
            for (int n = 0; n < split.Length; n++)
            {
                iaux[n] = Convert.ToInt32(split[n]);
            }
            numTidals = iaux.Length;
            p++;

            //Número de Materiais
            while ((Convert.ToString(lines[p][0]) + Convert.ToString(lines[p][1]) + Convert.ToString(lines[p][2])) != "set")
            {
                p++;
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            split = lines[p].Split(separators_materials, StringSplitOptions.RemoveEmptyEntries);
            material = new Material[split.Length - 2];
            for (int n = 0; n < split.Length - 2; n++)
            {
                material[n] = new Material(); 
                material[n].setName(split[n + 2]);
            }
            numMaterials = material.Length;
            p++;

            //Número de Berços
            while ((Convert.ToString(lines[p][0]) + Convert.ToString(lines[p][1]) + Convert.ToString(lines[p][2])) != "set")
            {
                p++;
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            iaux = new int[split.Length];
            for (int n = 0; n < split.Length; n++)
            {
                iaux[n] = Convert.ToInt32(split[n]);
            }
            numBerths = iaux.Length;
            p++;

            //Velocidade do Berço
            while ((Convert.ToString(lines[p][0]) + Convert.ToString(lines[p][1]) + Convert.ToString(lines[p][2]) + Convert.ToString(lines[p][3]) + Convert.ToString(lines[p][4])) != "param")
            {
                p++;
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            p++;

            while ((Convert.ToString(lines[p][0]) + Convert.ToString(lines[p][1]) + Convert.ToString(lines[p][2]) + Convert.ToString(lines[p][3]) + Convert.ToString(lines[p][4])) != "param")
            {
                p++;
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            p++;

            berth = new Berth[numBerths];
            for (int n = 0; n < numBerths; n++)
            {
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                berth[n] = new Berth();
                berth[n].setBerthSpeed(Convert.ToInt32(split[1]));
                p++;
            }

            //Maré de Chegada
            while ((Convert.ToString(lines[p][0]) + Convert.ToString(lines[p][1]) + Convert.ToString(lines[p][2]) + Convert.ToString(lines[p][3]) + Convert.ToString(lines[p][4])) != "param")
            {
                p++;
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            p++;
            for (int n = 0; n < numShips; n++)
            {
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                ship[n].setTideArrival(Convert.ToInt32(split[1]));
                p++;
            }

            //Estoque Inicial
            while ((Convert.ToString(lines[p][0]) + Convert.ToString(lines[p][1]) + Convert.ToString(lines[p][2]) + Convert.ToString(lines[p][3]) + Convert.ToString(lines[p][4])) != "param")
            {
                p++;
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            p++;
            for (int n = 0; n < numMaterials; n++)
            {
                split = lines[p].Split(separators_materials, StringSplitOptions.RemoveEmptyEntries);
                material[n].setInitialStock(Convert.ToDouble(split[1], CultureInfo.InvariantCulture));
                p++;
            }

            //Consumo de Material por Maré
            while ((Convert.ToString(lines[p][0]) + Convert.ToString(lines[p][1]) + Convert.ToString(lines[p][2]) + Convert.ToString(lines[p][3]) + Convert.ToString(lines[p][4])) != "param")
            {
                p++;
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            p++;
            for (int n = 0; n < numMaterials; n++)
            {
                split = lines[p].Split(separators_materials, StringSplitOptions.RemoveEmptyEntries);
                material[n].setConsumption(Convert.ToDouble(split[1], CultureInfo.InvariantCulture));
                p++;

            }

            //Carga por Navio
            while ((Convert.ToString(lines[p][0]) + Convert.ToString(lines[p][1]) + Convert.ToString(lines[p][2]) + Convert.ToString(lines[p][3]) + Convert.ToString(lines[p][4])) != "param")
            {
                p++;
                split = lines[p].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            p++;
            for (int n = 0; n < numShips; n++)
            {
                split = lines[p].Split(separators_materials, StringSplitOptions.RemoveEmptyEntries);
                cargo = new double[split.Length];
                for (int m = 0; m < split.Length - 1; m++)
                {
                    cargo[m] = Convert.ToDouble(split[m + 1], CultureInfo.InvariantCulture);
                }
                ship[n].setCargo(cargo);
                p++;
            }             
            
        }

        public int getNumShips(){
            return numShips;
        }

        public int getNumTidals()
        {
            return numTidals;
        }

        public int getNumBerths()
        {
            return numBerths;
        }

        public int getNumMaterials()
        {
            return numMaterials;
        }

        public void printData() {
            Console.Write("data;\n");

            Console.Write("set N :=");
            for (int i = 1; i <= ship.Length; i++)
            {
                Console.Write(" " + i);
            }
            Console.Write(";\n");

            Console.Write("set M :=");
            for (int i = 1; i <= numTidals; i++)
            {
                Console.Write(" " + i);
            }
            Console.Write(";\n");

            Console.Write("set K :=");
            for (int i = 0; i < material.Length; i++)
            {
                Console.Write(" " + material[i].getName());
            }
            Console.Write(";\n");

            Console.Write("set L :=");
            for (int i = 1; i <= berth.Length; i++)
            {
                Console.Write(" " + i);
            }
            Console.Write(";\n");

            Console.Write("param Mares := " + numTidals + ";\n");

            Console.Write("param v :=\n");
            for (int i = 1; i < berth.Length; i++)
            {
                Console.Write(i + " " + berth[i-1].getBerthSpeed() + "\n");
            }
            Console.Write(berth.Length + " " + berth[berth.Length - 1].getBerthSpeed() + ";\n");

            Console.Write("param a :=\n");
            for (int i = 1; i < ship.Length; i++)
            {
                Console.Write(i + " " + ship[i - 1].getTideArrival() + "\n");
            }
            Console.Write(berth.Length + " " + ship[ship.Length - 1].getTideArrival() + ";\n");


            Console.Write("param e :=\n");
            for (int i = 1; i < material.Length; i++)
            {
                Console.Write(material[i - 1].getName() + " " + material[i - 1].getInitialStock() + "\n");
            }
            Console.Write(material[material.Length - 1].getName() + "\t" + material[material.Length - 1].getInitialStock() + "\n");

            Console.Write("param ck :=\n");
            for (int i = 1; i < material.Length; i++)
            {
                Console.Write(material[i - 1].getName() + " " + material[i - 1].getConsumption() + "\n");
            }
            Console.Write(material[material.Length - 1].getName() + "\t" + material[material.Length - 1].getConsumption() + "\n");

            Console.Write("param q : Bauxita Alumina Manganes Ferro :=\n");
            for (int i = 1; i < ship.Length; i++)
            {
                Console.Write(i);
                for (int j = 0; j < material.Length; j++)
                {
                    Console.Write("\t" + ship[i - 1].getCargo()[j]);
                }
                Console.Write("\n");
            }
            Console.Write(ship.Length);
            for (int j = 0; j < material.Length; j++)
            {
                Console.Write("\t" + ship[ship.Length - 1].getCargo()[j]);
            }
            Console.Write(";\n");
            Console.Write("end;\n");
        }
    }

}
