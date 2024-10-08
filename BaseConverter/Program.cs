using System.IO;
using System.Collections.Generic;

public class BaseInt{
    public int a; // Numeric base ---- main / numerator
    public int b = 0; // Numeric base ---- ignored / denominator
    public int value;

    public BaseInt(string s) {
        s += ' ';
        List<string> strings = new();
        string currentStr = "";
        foreach (char c in s){
            if (c == ' ') {
                strings.Add(currentStr);
                currentStr = "";
                continue;
            }
            currentStr += c;
        }
        /*foreach (string ss in strings) {
            Console.WriteLine('\"' + ss + '\"');
        }*/
        if (strings.Count < 3) {
            a = 0;
            Console.Write("Write in form \"v in a / b \"");
            return;
        }
        if (!int.TryParse(strings[0], out value)) {
            a = 0;
            Console.Write("Invalid declaration of value");
            return;
        }
        if (strings[1] != "in" || !int.TryParse(strings[2], out a)) {
            a = 0;
            Console.Write("Invalid declaration of int base");
            return;
        }
        if (strings.Count < 5) {
            b = 1;
            return;
        }
        if (strings[3] != "/" || !int.TryParse(strings[4], out b)) {
            b = 0;
            Console.Write("Invalid declaration of dividend base");
            return;
        }
    }

    public void Write(){
        if (a == 0) {Console.Write("Invalid declaration of base"); return;}
        if (b == a) {Console.Write("Base can't equal 1"); return;}
        string o;
        if (b == 1 || b == 0){
            // Convert normally
            int v = value / a;
            o = ""+ToBaseDigit(value - (v*a));
            while (v > 0) {
                int pv = v;
                v = v / a;
                o = ToBaseDigit(pv - (v*a)) + o;
            }
        } else {
            // Fractional bases
            // Handle n>1 and n<1 cases
            if (b < a) {
                // Calulate normally
                int v = value / a * b;
                o = ""+ToBaseDigit(value - (v*a/b));
                while (v > 0) {
                    int pv = v;
                    v = v / a * b;
                    o = ToBaseDigit(pv - (v*a/b)) + o;
                }
            } else {
                // Calulate normally but a and b swaped
                int v = value / b * a;
                o = ""+ToBaseDigit(value - (v*b/a));
                while (v > 0) {
                    int pv = v;
                    v = v / b * a;
                    o = ToBaseDigit(pv - (v*b/a)) + o;
                }
                // Flip decimal
                string ro = o[o.Length - 1] + ".";
                for (int i = o.Length - 2; i >= 0; i--) {
                    ro += o[i];
                }
                o = ro;
            }
        }
        Console.WriteLine($"{value} in base {((b != 1) ? $"{a}/{b}" : $"{a}")} is equal to {o}");
    }

    // For converting numbers with a base greater than 10, example 0xa in 16 == 10 in 10
    public char ToBaseDigit(int digit) {
        if (digit < 10) { 
            return (char)(48 + digit); // 0-9
        }
        return (char)(97 + (digit-10)); // n>10
    }

    public int FromBaseDigit(char c) {
        if ((int)c < 60) {
            return (int)c - 48; // 0-9
        }
        return (int)c - 97; // n>10
    }
}

public class BaseConverter {
    public static void Main(string[] args) {
        Console.Write("Welcome to BaseConverter\n\u00A9 DecCatBurner 2024\n");
        while (true) {
            Console.WriteLine("Type the number to convert and the base to convert to. (v in a / b)");

            string input = Console.ReadLine();

            if (input.ToUpper() == "EXIT") {goto EXIT;}

            BaseInt b = new BaseInt(input);
            b.Write();
        }

        EXIT:
        Console.WriteLine("You suck, kys");
    }
}