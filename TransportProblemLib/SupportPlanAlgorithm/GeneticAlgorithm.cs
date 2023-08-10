using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportProblemLib.Abstruct;
using TransportProblemLib.Extention;

namespace TransportProblemLib.SupportPlanAlgorithm
{
    class GeneticAlgorithm : TransportAlgorithm
    {
        private readonly double[,] _cost;
        private readonly int _populationSize;
        private readonly int _maxIter;
        private readonly double _mutationRate;

        public GeneticAlgorithm(double[] reserves, double[] needs, double[,] cost, int populationSize, int maxIter, double mutationRate)
            : base(reserves, needs)
        {
            _cost = cost;
            _populationSize = populationSize;
            _maxIter = maxIter;
            _mutationRate = mutationRate;
        }
        public override double[,] GetPlan()
        {
            // Create a population of random solutions
            List<List<List<double>>> population = new(_populationSize);
            for (int i = 0; i < _populationSize; i++)
            {
                population.Add(GetRandomSolution());
            }

            // Find the best solution in the population
            List<List<double>> bestSolution = population[0];
            double bestFitness = GetFitness(bestSolution);
            for (int i = 1; i < population.Count; i++)
            {
                double fitness = GetFitness(population[i]);
                if (fitness < bestFitness)
                {
                    bestSolution = population[i];
                    bestFitness = fitness;
                }
            }

            // Iterate for the specified number of times
            for (int i = 0; i < _maxIter; i++)
            {
                // Select two random solutions
                var parent1 = population[Random.Shared.Next(0, _populationSize)];
                var parent2 = population[Random.Shared.Next(0, _populationSize)];

                // Breed the two solutions
                var res = Crossover(parent1, parent2);

                var child1 = res.Item1;
                var child2 = res.Item2;

                // Mutate the two solutions
                child1 = Mutate(child1);
                child2 = Mutate(child2);

                // Evaluate the fitness of the two solutions
                var child1Fitness = GetFitness(child1);
                var child2Fitness = GetFitness(child2);

                // Replace the two parents with the two offspring if the offspring have a better fitness
                if (child1Fitness < bestFitness)
                {
                    bestFitness = child1Fitness;
                    bestSolution = child1;
                }
                if (child2Fitness < bestFitness)
                {
                    bestFitness = child2Fitness;
                    bestSolution = child2;
                }
            }

            // Return the best solution
            return bestSolution.ToMatrix();
        }

        private List<List<double>> GetRandomSolution()
        {
            List<List<double>> solution = new(Reserves.Length);
            for (int i = 0; i < Reserves.Length; i++)
            {
                solution.Add(new());
                for (int j = 0; j < Needs.Length; j++)
                {
                    solution[i].Add(Random.Shared.Next(0, (int)Needs[i]));

                }
            }
            return solution;
        }

        private double GetFitness(List<List<double>> solution)
        {
            double totalCost = 0;
            for (int i = 0; i < solution.Count; i++)
            {
                for (int j = 0; j < solution[i].Count; j++)
                {
                    totalCost += _cost[i, j] * solution[i][j];
                }
            }
            return totalCost;
        }

        (List<List<double>>, List<List<double>>) Crossover(List<List<double>> solution1, List<List<double>> solution2)
        {
            List<List<double>> child1 = new();
            List<List<double>> child2 = new();

            for (int i = 0; i < solution1.Count; i++)
            {
                List<double> child1Row = solution1[i].Take(solution1[i].Count / 2)
                                                 .Concat(solution2[i].Skip(solution1[i].Count / 2))
                                                 .ToList();
                child1.Add(child1Row);

                List<double> child2Row = solution2[i].Take(solution1[i].Count / 2)
                                                 .Concat(solution1[i].Skip(solution1[i].Count / 2))
                                                 .ToList();
                child2.Add(child2Row);
            }

            return (child1: child1, child2: child2);
        }

        List<List<double>> Mutate(List<List<double>> solution)
        {
            for (int i = 0; i < solution.Count; i++)
            {
                for (int j = 0; j < solution[i].Count; j++)
                {
                    if (Random.Shared.NextDouble() < _mutationRate)
                    {
                        solution[i][j] = Random.Shared.Next(0, (int)Needs[i] + 1);
                    }
                }
            }
            return solution;
        }
    }
}
