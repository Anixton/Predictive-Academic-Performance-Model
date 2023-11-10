using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puan_Tahmini
{
    public class Neuron
    {
        private double weightOfStudy;
        private double weightOfAttendance;
        private double bias;
        private double mse;

        // doubleArray[Çalışma Süresi, Derse Devam, Sınav Sonucu]
        public double[,] doubleArray = new double[21, 3]
        {
            {7.6, 11, 77},
            {8, 10, 70},
            {6.6, 8, 55},
            {8.4, 10, 78},
            {8.8, 12, 95},
            {7.2, 10, 67},
            {8.1, 11, 80},
            {9.5, 9, 87},
            {7.3, 9, 60},
            {8.9, 11, 88},
            {7.5, 11, 72},
            {7.6, 9, 58},
            {7.9, 10, 70},
            {8, 10, 76},
            {7.2, 9, 58},
            {8.8, 10, 81},
            {7.6, 11, 74},
            {7.5, 10, 67},
            {9, 10, 82},
            {7.7, 9, 62},
            {8.1, 11, 82}
        };

        public Neuron(double weight1, double weight2)
        {
            weightOfStudy = weight1;
            weightOfAttendance = weight2;

            bias = 0;
            mse = 0;

            for (int i = 0; i < 21; i++)
            {
                doubleArray[i, 0] /= 10;    // Divide the first column by 10
                doubleArray[i, 1] /= 15;    // Divide the second column by 15
                doubleArray[i, 2] /= 100;   // Divide the last column by 100
            }

        }

        public double ComputeOutput(double studyTime, double attendance)
        {
            double examResultPrediction = (studyTime * weightOfStudy) + (attendance * weightOfAttendance) + bias;
            return examResultPrediction;
        }

        
        public void Train(double learningRate, int epochs)
        {
            for (int epoch = 0; epoch < epochs; epoch++)
            {
                double totalError = 0;

                for (int i = 0; i < doubleArray.GetLength(0); i++)
                {
                    double studyTimeInput = doubleArray[i, 0];
                    double attendanceInput = doubleArray[i, 1];
                    double target = doubleArray[i, 2];

                    double output = ComputeOutput(studyTimeInput, attendanceInput);
                    double error = target - output;

                    double deltaWeightOfStudy = learningRate * error * studyTimeInput;
                    double deltaWeightOfAttendance = learningRate * error * attendanceInput;
                    double deltaBias = learningRate * error;

                    weightOfStudy += deltaWeightOfStudy;
                    weightOfAttendance += deltaWeightOfAttendance;
                    bias += deltaBias;

                    totalError += error;
                    mse+= error * error;
                }

                mse /= doubleArray.GetLength(0);

                Console.WriteLine($"Epoch {epoch + 1}, Total Error: {totalError}, MSE: {this.getMSE()}");
            }
        }

        public double getMSE()
        {
            return mse;
        }
    }

    internal class Program
    {
        

        static void Main(string[] args)
        {
            double[] inputs = { 2.0, 3.0 };

            // random positive values between 0.0 and 1
            int seed = 42;
            Random random = new Random(seed);
            double randomDouble1 = random.NextDouble(); // Generates a random double between 0 (inclusive) and 1 (exclusive)
            double randomDouble2 = random.NextDouble(); // Generates another random double

            double[] weights = { randomDouble1 , randomDouble2};

            Neuron ANN = new Neuron(randomDouble1, randomDouble1);

            Console.WriteLine(ANN.ComputeOutput(inputs[0], inputs[1]));

            ANN.Train(0.005, 10);


            // Display column labels with fixed-width columns
            Console.WriteLine("First Data         Second Data        Target Value       Predicted Output");

            // Format and print the model's output for the same data with fixed-width columns
            for (int i = 0; i < ANN.doubleArray.GetLength(0); i++)
            {
                double input1 = ANN.doubleArray[i, 0];
                double input2 = ANN.doubleArray[i, 1];
                double target = ANN.doubleArray[i, 2];
                double predictedOutput = ANN.ComputeOutput(input1, input2);

                // Use fixed-width columns for each value
                Console.WriteLine($"{input1,-20:F2}  {input2,-20:F2}  {target,-20:F2}  {predictedOutput,-20:F2}");
            }

            // returns Mean Square Error
            Console.WriteLine(ANN.getMSE());

            // write MSE formula 
            // In Statistics, Mean Squared Error (MSE) is defined as Mean or
            // Average of the square of the difference between actual and estimated values.

            //MSE is used to check how close estimates or forecasts are to actual values. Lower the MSE, the closer is forecast to actual.
            //This is used as a model evaluation measure for regression models and the lower value indicates a better fit
            //purpose of mse above

            // resource
            // https://www.mygreatlearning.com/blog/mean-square-error-explained/#:~:text=In%20Statistics%2C%20Mean%20Squared%20Error,between%20actual%20and%20estimated%20values.

        }
    }
}
