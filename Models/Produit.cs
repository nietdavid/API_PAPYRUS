namespace API_PAPYRUS.Models
{
    public class Produit
    {
        public string Codart { get; set; } = "";
        public string Libart { get; set; } = "";
        public short Stkale { get; set; }
        public short Stkphy { get; set; }
        public short Qteann { get; set; }
        public string Unimes { get; set; } = "";

        public Produit() { }
        public Produit(string codart, string libart, short stkale, short stkphy, short qteann, string unimes)
        {
            Codart = codart;
            Libart = libart;
            Stkale = stkale;
            Stkphy = stkphy;
            Qteann = qteann;
            Unimes = unimes;
        }
    }
}
