using System;
namespace GeneticAlgorithm{

    public class Chromosome : IChromosome{
        private int[] _genes;
        private double _fitness;
        private Random _rnd;
        Chromosome(int numOfGenes, int potSeed){
            genes = new int[numOfGenes];
            rnd = new Random(potSeed);
        }
        // Chromosome(){
        //     Chromosome duplicated = new Chromosome()
        //     Chromosome duplicated= aChromosome;
        // }
        public double Fitness{
            get{
                return this.fitness;
            }
        }
        public int[] Genes{
            get{
                return this.genes;
            }
        }
        public long Length{
            get{
                return Genes.Length;
            }
        }
        public int this[int index]{
            get{
                return Genes[index];

            }
            set{
                Genes[index] = value;
            }

        }
        public int[] FillChromsome(){
            for(var i =0; i < Genes.Length; i++){
                var randNum = rnd.Next(0,6);
                Genes[i] = randNum;
            }
            return this.Genes;
        }
        private Chromosome mutate(Chromosome child, double mutationRate){
            //ex mutationRate 0.2
            double[] mutationArr = new double[100];
            for(var i =0;i<mutationArr.Length;i++){
                mutationArr[i]= rnd.NextDouble();
            }
            for(var j=0; j< child.Length;j++){
                if(mutationRate < mutationArr[j]){
                    child[j] = rnd.Next(0,6);
                }
            }
            return child;
        }
        public IChromosome[] Reproduce(IChromosome spouse, double mutationRate){
            var lowerBound = rnd.Next(0,121);
            var upperBound = rnd.Next(121,243);
            if (mutationRate > 1 || mutationRate < 0){
                throw new ArgumentException("Mutation rate should me between 0 and 1");
            }
            Chromosome[] childChromosomes = new Chromosome[2]; 
            Chromosome child1 = new Chromosome(243,5);
            Chromosome child2 = new Chromosome(243,4);
            for(var i=0; i<spouse.Length;i++){
                if(i >= lowerBound && i <= upperBound){
                    child1[i]=this.Genes[i];
                }
                child1[i]= spouse[i];
            }
            for(var j=0; j<this.Genes.Length;j++){
                if(j>= lowerBound && j<= upperBound){
                    child2[j]= spouse[j];
                }
                child2[j] = this.Genes[j];
            }
        
            childChromosomes[0]= this.mutate(child1,mutationRate);
            childChromosomes[1]=this.mutate(child2,mutationRate);
            return childChromosomes;     
        }




    }
}