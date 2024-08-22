using Mossad.Modles;
namespace Mossad.SyetemMap
{
    public static class Move
    { 
        
        ///ליצור אינטר פייס כדי שהוא יוכל לקבל את המיקום
        
        public static void Moveeng (object obj, string direction)
        {
            switch (direction)
                    {
                        //צפון מערב
                        case "nw":
                            obj.Location.X -= 1;
                            obj.Location.Y -= 1;
                            break;
                        //צפון
                        case "n":
                            target.Location.Y -= 1;
                            break;
                        // צפון מזרח
                        case "ne":
                            target.Location.X += 1;
                            target.Location.Y -= 1;
                            break;
                        //מזרח
                        case "e":
                            target.Location.X += 1;
                            break;
                        // דרום מזרח
                        case "se":
                            target.Location.X += 1;
                            target.Location.Y += 1;
                            break;
                        //דרום 
                        case "s":
                            target.Location.Y += 1;
                            break;
                        // דרום מערב
                        case "sw":
                            target.Location.X -= 1;
                            target.Location.Y += 1;
                            break;
                        // מערב
                        case "w":
                            target.Location.X -= 1;
                            break;
        }
    }
}
