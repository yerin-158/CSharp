using System;

namespace CSharp
{
    class Program 
    {
        enum ClassType { 
            None = 0,
            Night = 1,
            Archer = 2,
            Mage = 3
        }

        struct Player {
            public int hp;
            public int attack;
            public ClassType type;

            public void info() {
                Console.WriteLine("직업: {0}, hp: {1}, attack: {2}", this.type, this.hp, this.attack);
            }

            public void printHp() {
                Console.WriteLine("Player HP: {0}", this.hp);
            }

            public void hillingHp() {
                switch (this.type) {
                    case ClassType.Night:
                        this.hp = 100;
                        break;
                    case ClassType.Archer:
                        this.hp = 75;
                        break;
                    case ClassType.Mage:
                        this.hp = 50;
                        break;
                }
            }
        }

        static ClassType choiceClass() {
            Console.WriteLine("플레이어를 생성하세요");
            Console.WriteLine("[1] 기사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 법사");

            string choice = Console.ReadLine();
            if (choice == null)
            {
                return ClassType.None;
            }

            switch (choice) {
                case "1":
                    return ClassType.Night;
                case "2":
                    return ClassType.Archer;
                case "3":
                    return ClassType.Mage;
                default:
                    return ClassType.None;
            }
        }

        static void createPlayer(ClassType type, out Player player) {
            switch (type) {
                case ClassType.Night:
                    player.hp = 100;
                    player.attack = 10;
                    player.type = type;
                    break;
                case ClassType.Archer:
                    player.hp = 75;
                    player.attack = 12;
                    player.type = type;
                    break;
                case ClassType.Mage:
                    player.hp = 50;
                    player.attack = 15;
                    player.type = type;
                    break;
                default:
                    player.hp = 10;
                    player.attack = 10;
                    player.type = ClassType.None;
                    break;
            }
        }

        static void EnterGame(ref Player player) {
            while (true)
            {
                Console.WriteLine("마을에 접속했습니다.");

                Console.WriteLine("[1] 필드로 간다");
                Console.WriteLine("[2] 로비로 돌아간다");
                Console.WriteLine("[3] 체력을 회복한다");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("필드로 나갑니다.");
                        EnterField(ref player);
                        break;
                    case "2":
                        Console.WriteLine("캐릭터를 다시 선택합니다.");
                        return;
                    case "3":
                        Console.WriteLine("체력을 회복합니다... 현재 체력 {0}", player.hp);
                        player.hillingHp();
                        Console.WriteLine("체력이 회복되었습니다... 현재 체력 {0}", player.hp);
                        break;
                }
            }
        }

        enum MonsterType { 
            None = 0,
            Slime = 1,
            Orc = 2,
            Skeleton = 3
        }

        struct Monster {
            public int hp;
            public int attack;
            public MonsterType type;

            public void info()
            {
                Console.WriteLine("타입: {0}, hp: {1}, attack: {2}", this.type, this.hp, this.attack);
            }

            public void printHp()
            {
                Console.WriteLine("Monster HP: {0}", this.hp);
            }
        }

        static void createMonster(out Monster monster) {
            Random rand = new Random();
            int num = rand.Next(1, 4);

            switch (num)
            {
                case (int)MonsterType.Slime:
                    Console.WriteLine("슬라임이 등장했습니다.");
                    monster.type = MonsterType.Slime;
                    monster.hp = 20;
                    monster.attack = 2;
                    break;
                case (int)MonsterType.Orc:
                    Console.WriteLine("오크가 등장했습니다.");
                    monster.type = MonsterType.Orc;
                    monster.hp = 40;
                    monster.attack = 4;
                    break;
                case (int)MonsterType.Skeleton:
                    Console.WriteLine("스켈레톤이 등장했습니다.");
                    monster.type = MonsterType.Skeleton;
                    monster.hp = 30;
                    monster.attack = 3;
                    break;
                default:
                    monster.type = MonsterType.None;
                    monster.hp = 0;
                    monster.attack = 0;
                    break;
            }
        }

        static void EnterField(ref Player player) {
            while (true)
            {
                Monster monster;

                createMonster(out monster);

                Console.WriteLine("[1] 싸운다.");
                Console.WriteLine("[2] 도망간다.");

                String choice = Console.ReadLine();

                if (choice == "1")
                {
                    Fight(ref player, ref monster);
                }
                else if (choice == "2")
                {
                    Console.WriteLine("일정 확률로 도망에 성공합니다.");
                    Random rand = new Random();
                    int num = rand.Next(0, 2);
                    if (num == 0)
                    {
                        Console.WriteLine("도망에 실패하였습니다.");
                        Fight(ref player, ref monster);
                    }
                    else {
                        Console.WriteLine("도망에 성공하셨습니다.");
                    }

                    break;
                }
            }
        }

        static void Fight(ref Player player, ref Monster monster) {
            Console.WriteLine("전투를 시작합니다.");
            bool playerTurn = true;

            do
            {
                if (playerTurn)
                {
                    Console.WriteLine("========================================================================");
                    Console.WriteLine("플레이어가 몬스터에게 {0} 만큼의 피해를 입혔습니다.", player.attack);
                    monster.hp = monster.hp - player.attack > 0 ? monster.hp - player.attack : 0;
                    player.printHp();
                    monster.printHp();

                    playerTurn = !playerTurn;
                }
                else {
                    Console.WriteLine("========================================================================");
                    Console.WriteLine("몬스터가 플레이어에게 {0} 만큼의 피해를 입혔습니다.", monster.attack);
                    player.hp = player.hp - monster.attack > 0 ? player.hp - monster.attack : 0;
                    player.printHp();
                    monster.printHp();

                    playerTurn = !playerTurn;
                }


            } while (player.hp != 0 && monster.hp != 0);


            Console.WriteLine("========================================================================");
            Console.WriteLine("{0}와의 전투에서 {1}", monster.type, player.hp == 0 ? "패배했습니다" : "승리했습니다.");
            Console.WriteLine("========================================================================");
        }

        static void Main(string[] args)
        {
            while (true)
            {
                ClassType myClass = choiceClass();
                Player player = new Player();

                createPlayer(myClass, out player);
                player.info();

                EnterGame(ref player);
            }
        }
    }
}