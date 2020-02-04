using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monopol_game
{
    class Program
    {
        public static string[] fillArrGameBord()
        {
            string[] gameBoard = { "start", " ", " ", "Tel Aviv Center", "Tel Aviv Beach", " ", " ", " ", "Road 6", "Mifal Pais", " ", "Petah Tikva", "Mifal Petah Tikva", " ", " ", "Prison", " ", "Jerusalem", "Haifa", "Haifa", " ", "Police", " ", " ", "Eilat", "Mifal Eilat", " ", "Toto Winner", " ", " " };
            return gameBoard;
        }
        public static int[] fillArrPrice()
        {
            int[] price = { 0, 0, 0, 250, 300, 0, 0, 0, 50, 200, 0, 200, 50, 0, 0, 0, 0, 200, 150, 100, 0, 200, 0, 0, 200, 50, 0, 0, 0, 0 };
            return price;
        }
        public static int[] fillArrOwner()
        {
            int[] owner = new int[30];
            return owner;
        }

        public static int[] initArr(int [] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = -1;
            }
            return arr;
        }
        public static int RandomKubia()
        {
            int rnd_1 = 0, rnd_2 = 0, theKubeSum = 0;
            Random rnd = new Random();
            rnd_1 = rnd.Next(1, 7);
            rnd_2 = rnd.Next(1, 7);
            theKubeSum = rnd_1 + rnd_2;
            return theKubeSum;
        }
        public static int[] walletInit(int playersNum)
        {
            int[] wallet = new int[playersNum];
            for (int i = 0; i < playersNum ; i++)
            {
                wallet[i] = 1000;
            }
            return wallet;

        }
        public static bool isWalletEmpty(int [] wallet,string[] playersName)
        {
            bool isFiniShed = false;
            for (int i = 0; i < wallet.Length - 1; i++)
            {
               if (wallet[i] <= 0)
                {
                    Console.WriteLine("player number {0} named {1} lost because his wallet is empty  ",i+1, playersName[i]);
                    isFiniShed = true;
                }
            }
            return isFiniShed;

        }
        public static string[] enterPlayerName(int playersNum)
        {
            string[] name = new string[playersNum];
            for (int i = 0; i < playersNum ; i++)
            {
                Console.WriteLine("enter the name of the {0} player", i + 1);
                name[i] = Console.ReadLine();

            }
            return name;
        }
        public static int positionGame(int theKubeSum,int playerCurentPosition)
        {
            int newPosition = playerCurentPosition + theKubeSum;
           if (newPosition > 29)//we start from zero
            {
                newPosition -= 30;//ther is 30 steps on the map
            }
            return newPosition;
            
        }
        public static int NumOfPlayers()
        {
            Console.WriteLine("how match players are you 2-4?");
            int players = int.Parse(Console.ReadLine());
            while (players > 4 || players < 2)
            {
                Console.WriteLine("I see you didnt understend. how match players you are 2-4?");
                players = int.Parse(Console.ReadLine());
            }
            return players;
        }
        public static void PrintPlaces(int[] wallet,string[] playersName)
        {
            int max = 0;
            int first = 0;
            int second = 0;
            int third = 0;
            for (int i = 0; i < wallet.Length; i++)
            {
                if (wallet[i] > max)
                {
                    max = wallet[i];
                    first = i;

                }
            }
            if (wallet[first] > 0) 
                Console.WriteLine("the winner is {0} and his balance is {1}", playersName[first], wallet[first]);
            
            max = 0;
            for (int i = 0; i < wallet.Length; i++)
            {
                if (wallet[i] > max && i != first)
                {
                    max = wallet[i];
                    second = i;

                }
            }
            if (wallet[second] > 0 && wallet.Length >= 2)
                Console.WriteLine("the second place is {0} and his balance is {1}", playersName[second], wallet[second]);
            max = 0;
            for (int i = 0; i < wallet.Length; i++)
            {
                if (wallet[i] > max && i != first && i != second)
                {
                    max = wallet[i];
                    third = i;

                }
            }
            if (wallet[third] > 0 && wallet.Length>=3)
                Console.WriteLine("the third place is {0} and his balance is {1}", playersName[third], wallet[third]);
        }
        //print player details after the end of the round
        public static void printPlayerDetails(int wallet,string playersName,int punishment,int playerIndex,int[]owner,string[] gameBoard)
        {

            string cityName="";
            for (int i = 0; i < owner.Length; i++)//build a string with name of citys that he bought
            {
                if (owner[i] == playerIndex)
                    cityName = cityName + gameBoard[i]+",";
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("player number {0} named {1}  has {2} in wallet and has {3} punishments and you owner of {4} ", playerIndex + 1, playersName, wallet, punishment, cityName);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void gameRound( int player,int[] punishment,string[] playersName,int[] playerPosition, string[]gameBoard,int[] walletPleyers,int[] price,int[] owner)
        {
           

            if (punishment[player] == 0)
            {
                Console.WriteLine("player number {0} named {1} press enter to Cast a cube  ", player + 1, playersName[player]);
                Console.ReadLine();
                int sumKubia = RandomKubia();
                int newPosition = positionGame(sumKubia, playerPosition[player]);//calculate the new position by curent position + sum kubia
                playerPosition[player] = newPosition;
                Console.WriteLine("player number {0} named {1} you throwed number {2} and you move to position {3} ", player + 1, playersName[player], sumKubia, newPosition);
                string mapName = gameBoard[newPosition];
                if (mapName == "Road 6" || mapName == "Police Punishment")
                {
                    Console.WriteLine("you arrived to {0} u pay {1}", mapName, price[newPosition]);
                    walletPleyers[player] -= price[newPosition];
                }
                else if (mapName == " ")
                {
                    Console.WriteLine("you dont pay to anybody");
                }
                else if (mapName == "Mifal Pais")
                {
                    Console.WriteLine("you arrived to {0} you got {1}", mapName, price[newPosition]);
                    walletPleyers[player] += price[newPosition];

                }
                else if (mapName == "Mifal Petah Tikva" || mapName == "Mifal Eilat")
                {
                    Console.WriteLine("you arrived to {0} you got {1} but you have punishment to stay one round", mapName, price[newPosition]);
                    walletPleyers[player] += price[newPosition];
                    punishment[player] += 1;
                }

                else if (mapName == "Toto Winner")
                {

                    newPosition = positionGame(5, playerPosition[player]);
                    playerPosition[player] = newPosition;
                    Console.WriteLine("you moved 5 steps forward and your new position {0} ", newPosition);
                }
                else if (mapName == "Prison")
                {
                    Console.WriteLine("you arrived to the prison you will be punished for two rounds");
                    punishment[player] += 2;
                }
                else if (mapName == "Police")
                {
                    Console.WriteLine("you arrived to {0} you got {1} but you have punishment to pay 200 shekels to police", mapName, price[newPosition]);
                    walletPleyers[player] -= price[newPosition];

                }
                else //city
                {
                    int ownerPlayer = owner[newPosition];
                    if (ownerPlayer == -1)// if ther is no owner
                    {
                        Console.WriteLine("{0} ,the city {1} is empty you want to buy({2}) or skip  y,n?", playersName[player], mapName, price[newPosition]);
                        string yesOrNot = Console.ReadLine();
                        if (yesOrNot == "y")
                        {
                            walletPleyers[player] -= price[newPosition];
                            owner[newPosition] = player;
                            Console.WriteLine("you bought the city {0}", mapName);
                        }
                    }
                    else
                    {
                        int rentPay =(int )((price[newPosition] * 20) / 100);
                        Console.WriteLine("{0} ,the city {1} owned by {2} u pay rent 20% ({3}) ?", playersName[player], mapName, playersName[ownerPlayer], rentPay);
                        walletPleyers[player] -= rentPay;
                        walletPleyers[ownerPlayer] += rentPay;
                    }
                }


            }
            else
            {
                Console.WriteLine("player number {0} named {1} you are punished, you have to skip the round   ", player + 1, playersName[player]);
                Console.ReadLine();
                punishment[player] -= 1;
            }
            printPlayerDetails(walletPleyers[player], playersName[player], punishment[player], player,owner,gameBoard);
        }
        static void Main(string[] args)
        {
            //init game arrs
            string[] gameBord = fillArrGameBord();
            int[] price = fillArrPrice();
            int[] owner = fillArrOwner();
            owner =initArr(owner);//put -1 because 0 is a plyer number
            int numPlayers = NumOfPlayers();//enter players number 2-4
            int[] punishment = new int[numPlayers];
            int[] playerPosition = new int[numPlayers];
            string[] playersName = enterPlayerName(numPlayers);//enter players name
            int[] walletPleyers = walletInit(numPlayers);//put 1000 in each player wallet
            
            bool isEndGame = false;

            while(!isEndGame )
            {
                for(int player=0;player< numPlayers; player++)
                {
                    //the main fanction that it will be throw the kubs, put money,buy cityes,print,resualts
                    gameRound(player, punishment, playersName, playerPosition, gameBord, walletPleyers, price, owner);
                }
                //check if end game 
                isEndGame = isWalletEmpty(walletPleyers, playersName);
                if (!isEndGame)
                {
                    Console.WriteLine("you want to continue the game y,n");
                    string yesOrNot = Console.ReadLine();
                    if (yesOrNot == "n")
                    {
                        isEndGame = true;
                    }
                }
                
            }
            PrintPlaces(walletPleyers,playersName);//print who is first second third 
            Console.WriteLine("game over");
            Console.ReadLine();
        }
    }
}
