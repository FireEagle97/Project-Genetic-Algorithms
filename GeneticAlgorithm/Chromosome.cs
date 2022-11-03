using System;
namespace GeneticAlgorithm{

    public class Chromosome : IChromosome{
        private int[] _genes;
        private double _fitness;
        private Random _rnd;
        public Chromosome(int numOfGenes, int potSeed){
            NumOfGenes = numOfGenes;
            PotSeed = potSeed;
            _genes = new int[NumOfGenes];
            _rnd = new Random(PotSeed);
            Fitness = _fitness;
            this.FillChromsome();
        }
        public Chromosome(){
            Chromosome duplicated = new Chromosome(this.NumOfGenes, this.PotSeed);
        }
        public double Fitness{
            get{
                return this._fitness;
            }
            set{
                this._fitness =value;
            }
        }
        public int NumOfGenes{
            get;
        }
        public int PotSeed{
            get;
        }
        public int[] Genes{
            get{
                return this._genes;
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
        private int[] FillChromsome(){
            for(var i =0; i < Genes.Length; i++){
                var randNum = _rnd.Next(0,6);
                Genes[i] = randNum;
            }
            return this.Genes;
        }

        public IChromosome[] Reproduce(IChromosome spouse, double mutationRate){
            var lowerBound = _rnd.Next(0,121);
            var upperBound = _rnd.Next(121,243);
            if (mutationRate >= 10 || mutationRate < 0){
                throw new ArgumentException("Mutation rate should me between 0 and 10");
            }
            Chromosome[] childChromosomes = new Chromosome[2]; 
            Chromosome child1 = new Chromosome(243,5);
            Chromosome child2 = new Chromosome(243,4);
            
            for(var i=0; i<spouse.Length;i++){
                if(i >= lowerBound && i <= upperBound){
                    child1[i]=this.Genes[i];
                }
                child1[i]= spouse[i];
                double mutationProb = _rnd.NextDouble();
                //mutate
                // var newValue = _rnd.Next(0,100)
                //0.2
                if(mutationRate < mutationProb){
                    child1[i] = _rnd.Next(0,6);
                }
                
            }
            for(var j=0; j<this.Genes.Length;j++){
                if(j>= lowerBound && j<= upperBound){
                    child2[j]= spouse[j];
                }
                child2[j] = this.Genes[j];
                // double mutationProb = _rnd.NextDouble();
                // if(mutationRate < mutationProb){
                //     child1[j] = _rnd.Next(0,6);
                // }
                //mutate
                // var newValue = _rnd.Next(0,100);
                // if(newValue <= mutationRate){
                //     child1[j] = _rnd.Next(0,6);
                // }
                
            }
        
            childChromosomes[0]= child1;
            childChromosomes[1]=child2;
            return childChromosomes;     
        }

        public int CompareTo(IChromosome obj){
            if(obj == null){
                return 1;
            }
            Chromosome otherChromosome = obj as Chromosome;
            if (otherChromosome != null)
                return this.Fitness.CompareTo(otherChromosome.Fitness);
            else    
                throw new ArgumentException("object is not a Chromosome");    

        }

    }
}