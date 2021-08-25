using System;

namespace TextBasedGame {

    /**
    Below is a simple text game that runs off of chance.
    It allows the player to attack, drink health potions, or run.
    The player will encounter a random enemy on which they will fight to the death
    Unless the player is a coward and runs. 


    IMPROVEMENT:
    - We could add levels based on exp. Making the enemies have more health and health potion chance decreased/healing amount decreased.
    - We could add in a miss chance or a dodge chance.
    - We could add magic or other types of attacks that would have a chance of doing more or less damage
        + Maybe different kinds of weapons that is purchased or earned?
    - Store based on gold or exp? Buy weapons or more spells of some kind?
     */


    class Program {

        public static Random randomNumber = new Random();

        //Player variables
        public static int healthPotion = 3;
        public static int potionHealing = 25;
        public static int playerHealth = 100;
        public static int expEarned = 0;
        public static int playerAttack = 0;
    
        //Enemy variables
        public static int enemyAttack = 0;
        public static int enemyHealth = 0;
        public static string[] enemies = { "Hob", "Dragon", "Knight", "Demon", "Fallen Angel", "Serpent", "Skeleton" };
        public static string enemy = GetEnemy(enemies);


        static void Main(string[] args) {

            Boolean game = true;
            
            GAME:
            while (game) {
                Console.WriteLine("*****************************************************************");
                Console.WriteLine("\tWELCOME TO THE DUNGEON");
                Console.WriteLine("*****************************************************************");
                Console.WriteLine("Please input a number?\n" +
                    "\t1 - Play\n" +
                    "\t2 - Quit\n");

                //Gets the user input for GAME
                int mainInput = Int32.Parse(Console.ReadLine());

            FIGHTING:
                while (playerHealth > 0) {
                    switch (mainInput) {

                        case 1://Play
                            
                            Console.WriteLine("*****************************************************************");
                            Console.WriteLine($"\tA {enemy} has appeared to attack you!");
                            Console.WriteLine("*****************************************************************\n");
                            
                            enemyHealth = randomNumber.Next(20, 51);
                            Console.WriteLine($"\t{enemy}'s health is {enemyHealth}\n");
                            Console.WriteLine($"\tYou have {healthPotion} health potions left.\n");
                            Console.WriteLine($"\tYou have {playerHealth} HP left.\n");

                            while (enemyHealth > 0) {
                                Console.WriteLine("\tPlease input a number\n" +
                                "\t1 - Attack\n" +
                                "\t2 - Drink Health Potion\n" +
                                "\t3 - Run\n");
                                //Parses the players input into integer
                                int gameInput = Int32.Parse(Console.ReadLine());

                                switch (gameInput) {
                                    case 1://Attack

                                        enemyAttack = EnemyAttackAmount();
                                        playerHealth -= enemyAttack;
                                        Console.WriteLine("*****************************************************************");
                                        Console.WriteLine($"\t{enemy} hit you for {enemyAttack} HP.");
                                        Console.WriteLine($"\tYou have {playerHealth} HP left.\n");

                                        playerAttack = PlayerAttackAmount();
                                        enemyHealth -= playerAttack;
                                        expEarned += playerAttack;
                                        
                                        Console.WriteLine($"\tYou hit the {enemy} for {playerAttack} HP.");
                                        Console.WriteLine($"\t{enemy} has {enemyHealth} HP left.");
                                        Console.WriteLine("*****************************************************************\n");

                                        if (playerHealth < 1) {
                                            Console.WriteLine("\tYou have died.");
                                            game = false;
                                            goto GAME;
                                        }
                                        if (enemyHealth <= 0) {
                                            Console.WriteLine($"\tYou killed the {enemy}. Congrats! You earned {expEarned} EXP\n");
                                            int tempHealthPotion = healthPotion + HealthPotionChance();
                                            if (tempHealthPotion > healthPotion) {
                                                Console.WriteLine($"\tA health potion was dropped by the {enemy}.\n");
                                                Console.WriteLine($"\tYou now have {healthPotion} health potions\n");
                                            }
                                            //Console.WriteLine($"You have {healthPotion} health potions");
                                            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                                            Console.WriteLine($"\tYour total EXP is: {expEarned}");
                                            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\n\n");
                                            break;//case 1 gameInput
                                        }

                                        break;//case 1 gameInput

                                    case 2://Drink Health Potion
                                        Console.WriteLine("*****************************************************************");
                                        Console.WriteLine($"\tYou drank a health potion. You have {healthPotion} health potions left.\n");
                                        healthPotion--;
                                        playerHealth += potionHealing;
                                        Console.WriteLine($"\tThe potion healed you for {potionHealing} HP, you now have {playerHealth} HP left.");
                                        Console.WriteLine("*****************************************************************\n");
                                        break;//Case 2 gameInput

                                    case 3://Run
                                        Console.WriteLine("\tcase 3");
                                        //Jumps out of FIGHTING and moves to GAME
                                        goto GAME;

                                }
                            }
                            
                            break;//Break case 1 of fighting

                        case 2://Quit
                            game = false;
                            goto GAME;
                    }
                }
            }
            //End game good-bye
            Console.WriteLine("************************************************************************************************************");
            Console.WriteLine($"\tThank you for playing. You earned a total of {expEarned} EXP during your battles.");
            Console.WriteLine("************************************************************************************************************");
            System.Threading.Thread.Sleep(4000);

        }//end of main

        //Helper method for getting the names of enemies randomly
        public static string GetEnemy(string[] enemies) {
            Random randomEnemy = new Random();
            int enemyIndex = randomEnemy.Next(0, 7);
            //this should return a random index between 0-6
            return enemies[enemyIndex];
        }

        //Helper method for random health potion chance
        public static int HealthPotionChance() {
            Random potionChance = new Random();
            int chance = potionChance.Next(1, 11);
            if (chance < 11) {
                return healthPotion++;
            }
            return 0;
        }
        //Random amount of enemy-player attack
        public static int EnemyAttackAmount() {
            Random attackChance = new Random();
            int attackAmount = attackChance.Next(5, 31);
            return attackAmount;
        }
        //Random amount of player-enemy attack
        public static int PlayerAttackAmount() {
            Random attackChance = new Random();
            int attackAmount = attackChance.Next(5, 31);
            return attackAmount ;
        }
        //Experience earned if we wanted exp to be random.
        public static int ExperienceEarned() {
            int expEarned = PlayerAttackAmount();
            return expEarned;
        }
    }//end of class
}
