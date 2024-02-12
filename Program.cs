using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Predictive_Academic_Performance_Model
{
    public class Neuron
    {
        private double weightOfStudy;
        private double weightOfAttendance;
        private double bias;
        private double mse;

        // Our Data is stored in 2D Array
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

        // Constructor initializes the neural network with random weights and biases,
        // as well as normalizes the training data to ensure consistent scales for inputs and outputs.
        public Neuron()
        {

            // random positive values between 0.0 and 1
            Random random = new Random((int)DateTime.Now.Ticks);
            double randomDouble1 = random.NextDouble();
            double randomDouble2 = random.NextDouble();

            weightOfStudy = randomDouble1;
            weightOfAttendance = randomDouble2;

            bias = 0;
            mse = 0;

            for (int i = 0; i < 21; i++)
            {
                doubleArray[i, 0] /= 10;
                doubleArray[i, 1] /= 15;
                doubleArray[i, 2] /= 100;
            }

        }

        // Compute the predicted exam result based on study time, attendance, and current weights
        public double ComputeOutput(double studyTime, double attendance)
        {
            double examResultPrediction = (studyTime * weightOfStudy) + (attendance * weightOfAttendance) + bias;
            return examResultPrediction;
        }


        // Train the neuron using the provided learning rate and number of epochs
        public void Train(double learningRate, int epochs)
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            for (int epoch = 0; epoch < epochs; epoch++)
            {
                double totalError = 0;
                mse = 0;

                for (int i = 0; i < doubleArray.GetLength(0); i++)
                {
                    // Calculate the output of the linear regression model based on study time and attendance input,
                    // compute the error against the target value, and update weights and bias using gradient descent.
                    double studyTimeInput = doubleArray[i, 0];
                    double attendanceInput = doubleArray[i, 1];
                    double target = doubleArray[i, 2];

                    // Calculate the model output and the error
                    double output = ComputeOutput(studyTimeInput, attendanceInput);
                    double error = target - output;

                    // Update weights and bias using gradient descent
                    double deltaWeightOfStudy = learningRate * error * studyTimeInput;
                    double deltaWeightOfAttendance = learningRate * error * attendanceInput;
                    double deltaBias = learningRate * error;

                    weightOfStudy += deltaWeightOfStudy;
                    weightOfAttendance += deltaWeightOfAttendance;
                    bias += deltaBias;

                    // Track the total error and mean squared error
                    totalError += error;
                    mse += error * error;
                }

                mse /= doubleArray.GetLength(0);

                Console.WriteLine($"Epoch {epoch + 1}, Total Error: {totalError}, MSE: {this.getMSE()}");
            }
            Console.WriteLine("-----------------------------------------------------------------------");
            targetExpectedComparison();
            Console.WriteLine("-----------------------------------------------------------------------");
        }

        // Display the comparison between target and expected values
        private void targetExpectedComparison()
        {
            // Display column labels with fixed-width columns
            Console.WriteLine("Work          Attendance       Exam Result       Prediction");

            // Format and print the model's output for the same data with fixed-width columns
            for (int i = 0; i < this.doubleArray.GetLength(0); i++)
            {
                double input1 = this.doubleArray[i, 0];
                double input2 = this.doubleArray[i, 1];
                double target = this.doubleArray[i, 2];
                double predictedOutput = this.ComputeOutput(input1, input2);

                // Use fixed-width columns for each value             
                Console.WriteLine($"{input1 * 10,-15:F2}  {input2 * 15,-15:F2}  {target * 100,-15:F2}  {predictedOutput * 100,-15:F2}");


            }
            Console.WriteLine($"MSE: {this.getMSE()}");
        }

        public double getMSE()
        {
            return mse;
        }

        // Make predictions for given study time and attendance
        public void makePrediction(double studyTime, double attendanceTime)
        {
            double firstInput = (studyTime / 10);
            double secondInput = (attendanceTime / 15);

            double expectedVal = ComputeOutput(firstInput, secondInput) * 100;

            // Save the current console text color
            ConsoleColor originalColor = Console.ForegroundColor;

            // Set a new text color
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine($"Study Time: {studyTime}");
            Console.WriteLine($"Attendance: {attendanceTime}");
            Console.WriteLine($"Prediction: {expectedVal}");
            Console.WriteLine("-----------------------------------------------------------------------");

            // Restore the original console text color
            Console.ForegroundColor = originalColor;

        }
    }

    internal class Program
    {


        static void Main(string[] args)
        {
            // Note: This program is optimized for performance in release mode.
            // Debugging and development may be more effective in debug mode.
            // Be cautious when making changes and thoroughly test in both modes.

            // Training with 0.05 Learning Rate and 10 Epoches
            Neuron ANN = new Neuron();
            ANN.Train(0.05, 100);

            // Predicting exam results from unseen data using the model
            ANN.makePrediction(9, 8);
            ANN.makePrediction(7.5, 10);
            ANN.makePrediction(9, 9);
            ANN.makePrediction(8, 11);
            ANN.makePrediction(10, 10);


            // Comparing MSE values with different Learning Rate and Epoch Count
            Neuron[] ANN = new Neuron[9];
            for (int i = 0; i < ANN.Length; i++)
            {
                ANN[i] = new Neuron();
            }
            ANN[0].Train(0.01, 10);
            ANN[1].Train(0.01, 50);
            ANN[2].Train(0.01, 100);
            ANN[3].Train(0.025, 10);
            ANN[4].Train(0.025, 50);
            ANN[5].Train(0.025, 100);
            ANN[6].Train(0.05, 10);
            ANN[7].Train(0.05, 50);
            ANN[8].Train(0.05, 100);
            double[] mseArray = new double[9];

            for (int i = 0; i < mseArray.Length; i++)
            {
                mseArray[i] = ANN[i].getMSE();
            }

            for (int j = 0; j < mseArray.Length; j++)
            {
                Console.WriteLine();
                Console.WriteLine(mseArray[j]);
            }


            // In Statistics, Mean Squared Error (MSE) is defined as Mean or
            // Average of the square of the difference between actual and estimated values.

            //MSE is used to check how close estimates or forecasts are to actual values. Lower the MSE, the closer is forecast to actual.
            //This is used as a model evaluation measure for regression models and the lower value indicates a better fit

            // MSE Resource 
            // https://www.mygreatlearning.com/blog/mean-square-error-explained/#:~:text=In%20Statistics%2C%20Mean%20Squared%20Error,between%20actual%20and%20estimated%20values.


        }
    }
}
