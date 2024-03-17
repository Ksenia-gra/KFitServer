using KFitServer.DBContext.Models;

namespace KFitServer
{
    public record class ResponseProducts(List<Hint> Hints);

    public record class Hint(Food Food);

    public record class Food(string FoodId,string Label,Nutrients Nutrients);

    public record class Nutrients(double ENERC_KCAL,double PROCNT,double FAT, double CHOCDF);

    public record class JsonProduct(string id,string name,int calories, int carbohydrates, int proteins, int fats);

    public record class YPlaylist(List<Item> Items);

    public record class Item(string Id, Snippet Snippet);

    public record class Snippet(string Title, Thumbnails Thumbnails);

    public record class Thumbnails(Standard Standard);

    public record class Standard(string Url);

    public record class TranslateResponse(Data data);

    public record class Data(List<Translations> Translations);

    public record class Translations(string translatedText);
}
