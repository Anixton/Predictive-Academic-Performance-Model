

<h1>Predictive Academic Performance Model</h1>

<h2>Overview</h2>

<p>This repository contains a simple neural network implementation for predicting academic performance based on study time and attendance. The neural network is implemented in C# and uses a basic gradient descent algorithm for training.</p>

<h2>Neural Network Architecture</h2>

<p>The neural network consists of a single neuron with two input weights (study time and attendance) and a bias. The weights and bias are initialized with random values, and the training process adjusts them to minimize the mean squared error (MSE) between predicted and actual exam results.</p>

<h2>Usage</h2>

<h3>Training the Model</h3>

<p>To train the model, instantiate the <code>Neuron</code> class and call the <code>Train</code> method with the desired learning rate and number of epochs. Uncomment and modify the following code in the <code>Main</code> method to train the model:</p>

<pre>
// Training with 0.05 Learning Rate and 100 Epochs
Neuron ANN = new Neuron();
ANN.Train(0.05, 100);
</pre>

<h3>Making Predictions</h3>

<p>After training, you can make predictions using the trained model. Uncomment and modify the following code in the <code>Main</code> method to make predictions:</p>

<pre>
// Making predictions with the trained model
ANN.makePrediction(9, 8);
ANN.makePrediction(7.5, 10);
ANN.makePrediction(9, 9);
ANN.makePrediction(8, 11);
ANN.makePrediction(10, 10);
</pre>

<h3>Comparing MSE with Different Learning Rates and Epoch Counts</h3>

<p>The code includes a comparison of MSE values with different learning rates and epoch counts. Uncomment and modify the following code in the <code>Main</code> method to perform the comparison:</p>

<pre>
// Comparing MSE values with different Learning Rates and Epoch Counts
Neuron[] ANN = new Neuron[9];
for (int i = 0; i < ANN.Length; i++)
{
    ANN[i] = new Neuron();
}
ANN[0].Train(0.01, 10);
ANN[1].Train(0.01, 50);
// ... (continue with other configurations)
</pre>

<h2>Results</h2>

<p>The program prints the MSE and a comparison between target and predicted values for each training epoch. Additionally, it allows for making predictions on unseen data and comparing MSE values for different learning rates and epoch counts.</p>

<h2>Note</h2>

<ul>
    <li>Ensure that the program is optimized for performance in release mode for efficient execution.</li>
    <li>Debugging and development may be more effective in debug mode.</li>
</ul>

<h2>Resources</h2>

<ul>
    <li><a href="https://www.mygreatlearning.com/blog/mean-square-error-explained/">Mean Squared Error Explained</a>: Learn more about Mean Squared Error and its significance in model evaluation.</li>
</ul>

<p>Feel free to modify and experiment with the code to suit your needs. If you have any questions or suggestions, please open an issue or submit a pull request. Happy coding!</p>

</body>
</html>
