<h1>Neural Network</h1>
<b>Umut Osmanoglu</b></p>

I've done 2 different projects, I have the pacman ai project where I tried to do a generic algorithm with pacman, but given the complexity I gave up and did another project with a genetic algorithm.

<h2> Objectives </h2>

<p>
This project aims to create an artificial intelligence (AI) based on a neural network capable of playing pacman. Specific objectives include the implementation of an artificial neural network (ANN) with gradient backpropagation for learning.
</p>

<h2> Setting up the Neural Network </h2>

<h3> Technical Choices </h3>

First, I had to choose the type of neural network I was going to build. After much research, I came up with the idea of making a Pacman game using a genetic algorithm. The problem was the pacman AI because it had to learn the AI to know the whole map and once that was added, it had to add the ghosts knowing that each ghost has a different ground and a random paterne. 
So I'm planning to finish this Pacman project in my own time, which is why I have the beginning of Pacman. Knowing that with the lack of time I was going to have, I based myself on something a little simpler, teaching Agents to get out of a maze using the genetic algorithm.

<h3> Training sets </h3>

The success of the genetic algorithm depends largely on the quality of the training sets. I've built up data sets to enable my agents to solve the maze, and to do this I've simply managed several hundred agents, watching them evolve over several generations, and as they do so we'll automatically get behavior that's closer and closer to what we expect. Knowing that at the beginning we'll add agents randomly and see which ones do best with the rules we've set. Once the generation is complete, we'll take the best-behaved agents and use them as a basis for the next generation. But to move from one generation to another, we make mutations, there are several ways to make mutations, but personally, I based myself on a single neural network and I change a small amount of these weights to slightly change its behavior, it is through these mutations that agents will be able to test new things.

<h3> Settings </h3>

The Settings of the genetic algorithm, such as population size, mutation rate, and number of generations, were chosen after extensive experimentation. I made adjustments to optimize the convergence of the algorithm and avoid problems such as premature convergence.

<h3>Results</h3>

Honestly, I think the map of the maze is a bit big so that’s why my agents need a lot of time to find their way. At first, we see a difference between the 1st and the 20 generation, but after that it stagnates due to the fact that it is a maze and that there are several dead ends for them. And for the subjects of the Pacman, no it is not functional, it does not learn the map and blocks leading between 2 directions.

<h2> How to use the project ? </h2>

To launch the simulation, just launch the game by pressing the button at the top 

<h3> Training Scene </h3>

This is the main scene of the project. Brown walls block AI once touch and white walls help AI<br>

<h3> GameManager </h3>

If you click on the Save button, Save to save the weight of the agents and Load to load the weights of the agents, the timer is the time between each generation and population size is the number of agents

And then that’s it, they just remain for you to observe


<h2> Results and Possible Improvements </h2>

<h3> Network model </h3>

For Pacman instead of doing that of the genetic algorithm I should have specialized in the NEAT Algorithm 

    NEAT Algorithm alters both the weighting parameters and structures of networks, attempting to find a balance between the fitness of evolved solutions and their diversity.

Here’s an overview of what I did for pacman :

I generer more grid with pacman in it trying to learn the map, but I didn’t have time to finish.

And for the labyrinth, I should have better managed my mutations and my fitness (which tells me if the agents, we do a good thing or not for example if ever having points adds fitness, but waiting there removes fitness) It is above all these two things that I must learn better to coordinate to find a perfect balance for the learning of my agents. In addition, improve my save and load, because it happens sometimes when we load, the agents do anything.

<h3> Diversification of Situations </h3>

Add diversity to the database by creating more complex situations, introducing new elements or varying levels. This will help AI to generalize better in the face of unexpected challenges.

<h3> Network Load Optimization </h3>

Work on improving network backup and load mechanisms to avoid current problems. This may include adding integrity checks and better error management. Or even put our Agents in a world apart and see how they manage

<h3>Conclusion</h3>

By continuing to explore these improvements, the project could evolve into a more sophisticated, robust and effective solution for learning in the context of the Pacman game or maze. Implementation of these ideas should be iterative, with particular attention to analyzing the results to guide future improvements.
