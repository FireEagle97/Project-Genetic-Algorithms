using System;
namespace GeneticAlgorithm{

    public class Chromosome : IChromosome{
        private int[] genes;
        private double fitness;
        Chromosome(){
            genes = new int[243];
        }
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
        }
        public int[] FillChromsome(){
            Random rnd =new Random();
            for(var i =0; i < Genes.Length; i++){
                var randNum = rnd.Next(0,6);
                Genes[i] = randNum;
            }
            return this.Genes;
        }
        public IChromosome[] Reproduce(IChromosome spouse, double mutationRate){
            
            
        }




    }
}