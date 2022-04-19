namespace ProgrammingProject2
{
    //                                                                                  Marcus Herwig
    //                                                                                  Programming Project 2
    //                                                                                  Worst-Fit algorithm 
    class WorstFit
    {
        static void Main(string[] args)
        {
           MemoryArray mem = new MemoryArray(64);
            float[] percentages = new float[50];
            int requestSize;
            int count;
            Random rand = new Random();
            Process p1 = new Process(rand.Next(1, 1000), rand.Next(1,8));
            Process p2 = new Process(rand.Next(1, 1000), rand.Next(1,8));
            Process p3 = new Process(rand.Next(1, 1000), rand.Next(1,8));
            Process p4 = new Process(rand.Next(1, 1000), rand.Next(1,8));
            mem.makeSim(p1,p2,p3,p4);
            Process addP;
            for(int i = 0; i < 50; i++)
            {
                //mem.addToMemory(addP, 10);
                addP = new Process(rand.Next(1, 1000), rand.Next(1,8));
                mem.Request(addP);
                mem.release(rand.Next(0, mem.getArraySize()));
                Console.WriteLine("Memory Utilization: " + mem.determineMemoryUtil());
                percentages[i] = mem.determineMemoryUtil();
                
           }
            mem.display();
            Console.WriteLine("Memory blocks searched: " + mem.getSearchedCount());
            Console.WriteLine("Average memory utilization: " + percentages.Average());
        }
    }

    public class MemoryArray
    {
        int[] memArray;
        int size; 
        int searchedFits = 0;
        public MemoryArray(int size)
        {
            this.memArray = new int[size];
            this.size = size;
        }

         public void Request(Process p)
        {
            int b; 
            for(int i = 0; i < this.memArray.Length; i++ )
            {                  
                if(this.memArray[i] == 0)
                {
                    b = this.determineWorstFit();
                    if(b + p.getSize()  > this.size)
                    {
                        //Console.WriteLine("Allocation request cannot be granted"); // this line is wrong but i dont know what to replace it with 
                    }
                    else
                    {
                        if(this.memArray[b + p.getSize() - 1] == 0)
                         {
                      
                            this.addToMemory(p, this.determineWorstFit());
                        }
                    }
                    break;
                }

            }
        }
        public float determineMemoryUtil()
        {
            float count = 0;
            float percent = 0;
            for(int i = 0; i < this.size; i++)
            {
                if(this.memArray[i] != 0)
                {
                    count = count + 1;
                }

            }
            percent = count/this.size * 100;
            return percent;

        }
        private  int determineWorstFit()
        {
            int count = 0;
            int high = 0;
            int returnBucket = 0;
            int a;
           for(int i = 0; i < this.memArray.Length; i++ )
            {
                if(this.memArray[i] == 0)
                {
                    a = i;
                    
                    while(this.memArray[a] == 0 & a < this.memArray.Length-1)
                    {
                        count = count + 1;
                        a++;
                    }
                    if(count >= high)
                    {
                        returnBucket = i;
                        high = count;
                        count = 0;
                        this.searchedFits = this.searchedFits + 1;
                    }
                    else
                    {
                        count = 0;
                        this.searchedFits = this.searchedFits + 1;
                    }
                }
            }
            
            return returnBucket;

        }
        public int getSearchedCount()
        {
            return this.searchedFits;
        }
        public void release(int bucket)
        {
            int tempID = this.memArray[bucket];
            for(int i = 0;i < this.memArray.Length; i++ )
            {
                if(this.memArray[i] == bucket)
                {
                    this.memArray[i] = 0;
                }
            }
        }
        public int getArraySize()
        {
            return this.size;
        }
        public void display()
        {
            for(int i = 0; i < this.memArray.Length; i++)
            {
                Console.WriteLine(memArray[i]);
            }
        }

        public void makeSim(Process p, Process a, Process b, Process c)
        {
            for(int i = 0; i <= p.getSize(); i++)
            {
                this.memArray[i] = p.getID();
            }
            for(int x = 16; x <= a.getSize() + 16; x++)
            {
                this.memArray[x] = a.getID();
            }
            for(int y = 32; y <= b.getSize() + 32; y++)
            {
                this.memArray[y] = b.getID();
            }
            for(int z = 48; z <= c.getSize() + 48; z++)
            {
                this.memArray[z] = c.getID();
            } 
        }
        public void addToMemory(Process p, int spot)
        {
            for(int i = spot; i < p.getSize() + spot; i++)
            {
                this.memArray[i] = p.getID();
            }
        }
    }

    public class Process
    {
        int blockSize;
        int id;
        public Process(int id, int size)
        {
            this.blockSize = size;
            this.id = id;
        }

        public int getID()
        {
            return this.id;
        }

        public int getSize()
        {
            return this.blockSize;
        }
    
        

    }
}