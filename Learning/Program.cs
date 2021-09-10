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
        public static Random enemyRandomHealth = new Random();

        //Player variables
        public static int healthPotion = 3;
        public static int potionHealing = 25;
        public static int playerHealth = 100;
        public static int expEarned = 0;
        public static int playerAttack = 0;
        public static int level = 0;
    
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

                            //Set the amount of health randomly for an enemy
                            enemyHealth = enemyRandomHealth.Next(10, 31);

                            Console.WriteLine($"\t{enemy}'s health is {enemyHealth}\n");
                            Console.WriteLine($"\tYou have {healthPotion} health potions left.\n");

                            levelChecker(expEarned);

                            Console.WriteLine($"\tYou have {playerHealth} HP left.\n");
                            Console.WriteLine($"\tYou are level: {level}\n");

                            //We added this kind of an easter egg for when the player asks for potions when there are none. 3 times means punishment
                            int notEnoughPotionCounter = 0;

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

                                            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                                            Console.WriteLine($"\tYour total EXP is: {expEarned}\n");

                                            int tempLevel = level;
                                            levelChecker(expEarned);
                                            if (tempLevel < level) {
                                                Console.WriteLine($"\tLevel up! You are now level {level}!");
                                                levelChanger(level);
                                            }

                                            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\n");
                                            break;//case 1 gameInput
                                        }

                                        break;//case 1 gameInput

                                    case 2://Drink Health Potion
                                        
                                        if (healthPotion >= 0) {
                                            Console.WriteLine("*****************************************************************");
                                            Console.WriteLine($"\tYou drank a health potion. You have {healthPotion} health potions left.\n");

                                            healthPotion--;
                                            playerHealth += potionHealing;

                                            Console.WriteLine($"\tThe potion healed you for {potionHealing} HP, you now have {playerHealth} HP left.");
                                            Console.WriteLine("*****************************************************************\n");
                                        } else {
                                            Console.WriteLine("You do not have any health potions left. Fight or flight!");
                                            notEnoughPotionCounter++;
                                        } 
                                        
                                        if (notEnoughPotionCounter >=3) {
                                            playerHealth -= 30;
                                            Console.WriteLine("YOU HAVE NO POTIONS LEFT. DUE TO YOUR INCOMPENTENCE, YOU HAVE LOST 30 HEALTH!");
                                        }

                                        break;//to gameInput : Case 2

                                    case 3://Run
                                        Console.WriteLine($"\tYou ran away from the {enemy} like the coward you are.");
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
            Console.WriteLine($"\tThank you for playing. You earned a total of {expEarned} EXP during your battles and reached level {level}.");
            Console.WriteLine("************************************************************************************************************");
            //Shows the end screen for a few seconds before closing
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

        //We are useding this to determine the level of the player
        public static void levelChecker(int expEarned) {
            if (expEarned > 0 && expEarned < 100) {
                level = 1;
            } else if (expEarned >= 101 && expEarned < 400) {
                level = 2;
            } else if (expEarned >= 401 && expEarned< 800) {
                level = 3;
            } else if (expEarned >= 801 && expEarned < 1100) {
                level = 4;
            } else if (expEarned >= 1101 && expEarned < 1400) {
                level = 5;
            } else if (expEarned >= 1400) {
                level = 6;
            }

        }

        //This will change stats based upon the level of the player
        //The stats will need to be adjusted for fairness
        public static void levelChanger(int level) {
            
            if (level == 1) {
                enemyRandomHealth.Next(32, 41);
                if (playerHealth < 119) {
                    playerHealth = 120;
                }
                potionHealing = 30;
            } else if (level == 2) {
                enemyRandomHealth.Next(42, 51);
                if (playerHealth < 130) {
                    playerHealth = 130;
                }
                potionHealing = 40;
            } else if (level == 3) {
                enemyRandomHealth.Next(52, 61);
                if (playerHealth < 140) {
                    playerHealth = 140;
                }
                potionHealing = 45;
            } else if (level == 4) {
                enemyRandomHealth.Next(62, 71);
                if (playerHealth < 150) {
                    playerHealth = 150;
                }
                potionHealing = 50;
            } else if (level == 5) {
                enemyRandomHealth.Next(72, 81);
                if (playerHealth < 160) {
                    playerHealth = 160;
                }
                potionHealing = 55;
            } else if (level ==6) {
                enemyRandomHealth.Next(82, 91);
                if (playerHealth < 200) {
                    playerHealth = 200;
                }
                potionHealing = 70;
            }
        }

    }//end of class
}
