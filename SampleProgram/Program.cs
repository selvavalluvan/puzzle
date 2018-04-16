using System;
using System.Collections.Generic;


namespace SampleProgram
{
    public static class Program
    {
        // public static void Main()
        // {
        //     //var game = new Game();
        //     var game = new ComplexGame();
        //
        //     game.Setup();
        //     game.Play(1);
        //
        //     Console.WriteLine("Press any key ...");
        //     Console.ReadKey();
        // }
        public static void Main()
        {
            var testCard = AuthenticationMethod.FORMS;
             Console.WriteLine(testCard.ToString());
        }

        public sealed class Piece {

            private readonly String name;
            private readonly int value;

            public static readonly AuthenticationMethod FORMS = new AuthenticationMethod (1, "FORMSeee");
            public static readonly AuthenticationMethod WINDOWSAUTHENTICATION = new AuthenticationMethod (2, "WINDOWS");
            public static readonly AuthenticationMethod SINGLESIGNON = new AuthenticationMethod (3, "SSN");

            private AuthenticationMethod(int value, String name){
                this.name = name;
                this.value = value;
            }

            public override String ToString(){
                return name;
            }

        }
    }
}
