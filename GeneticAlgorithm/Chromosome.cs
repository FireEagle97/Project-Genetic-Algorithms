namespace GeneticAlgorithm{

    public class Chromosome : IChromosome{
        Chromosome(double fitness, int[] genes){
            Fitness = fitness;
            Genes = genes;
        }
        public double Fitness{
            get;
        }
        public int[] Genes{
            get;
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
        }
        public IChromosome[] Reproduce(IChromosome spouse, double mutationRate){
            
        }




    }
}