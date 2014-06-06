using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace APIFiMag
{
    #region Enums

    /// <summary>
    /// Choix entre historique des taux de change journalier, mensuel ou annuel
    /// </summary>
    public enum Frequency
    {
        Daily,
        Monthly,
        Yearly
    }

    /// <summary>
    /// Enumération des informations historiques que l'on peut demander
    /// Chacune prendra une colonne de données
    /// </summary>
    public enum HistoricalColumn
    {
        Open,
        High,
        Low,
        Close,
        Volume
    }

    /// <summary>
    /// énumération des devises connues par le programme
    /// </summary>
    public enum Currency
    {
        ADF, ADP,
        AED,
        AFA,
        AFN,
        AFR,
        ALL,
        AMD,
        ANG,
        AOA,
        AON,
        ARS,
        ATS,
        AUD,
        AWF,
        AWG,
        AZM,
        AZN,
        BAM,

        BBD,

        BDT,

        BEF,

        BGL,

        BGN,

        BHD,

        BIF,

        BMD,

        BND,

        BOB,

        BRL,

        BSD,

        BTN,

        BWP,

        BYR,

        BZD,

        CAD,

        CDF,

        CHF,

        CLP,

        CNY,

        COP,

        CRS,

        CUC,

        CUP,

        CVE,

        CYP,

        CZK,

        DEM,

        DIF,

        DKK,

        DOP,

        DZF,

        ECS,

        EEK,

        EGP,

        ERN,

        ESP,

        ETD,

        EUR,

        FIM,

        FJD,

        FKP,

        GBP,

        GEL,

        GGP,

        GHC,

        GHS,

        GIP,

        GMP,

        GNF,

        GRD,

        GTQ,

        GYD,

        HKD,

        HNL,

        HRK,

        HTG,

        HUF,

        IDR,

        IEP,

        ILS,

        IMP,

        INR,

        IQD,

        IRR,

        ISK,

        ITL,

        JEP,

        JMP,

        JOD,

        JPY,

        KES,

        KGS,

        KHR,

        KMF,

        KPW,

        KRW,

        KWD,

        KYD,

        KZT,

        LAK,

        LBP,

        LKR,

        LRD,

        LSL,

        LTL,

        LUF,

        LVL,

        LYD,

        MAD,

        MDL,

        MGA,

        MGF,

        MKD,

        MMK,

        MNT,

        MOP,

        MRO,

        MTL,

        MUR,

        MVR,

        MWK,

        MXN,

        MYR,

        MZM,

        MZN,

        NAD,

        NGN,

        NIO,

        NLG,

        NOK,

        NPR,

        NTD,

        NZD,
        OMR,

        PAB,

        PEN,

        PGK,

        PHP,

        PKR,

        PLN,

        PSL,

        PTE,

        PYG,

        QAR,

        ROL,
        RON,

        RSD,

        RUB,

        RWF,

        SAR,

        SBD,

        SCR,

        SDD,

        SDG,

        SDP,

        SEK,

        SGD,

        SHP,

        SIT,

        SKK,

        SLL,

        SOS,

        SPL,

        SRD,

        SRG,

        STD,

        SVC,

        SVP,

        SZL,

        THB,

        TJS,

        TMM,

        TND,

        TOP,

        TRL,

        TRY,

        TTD,

        TYD,

        TWD,

        TZS,

        UAH,

        UGX,

        USD,

        UYP,

        UYU,

        UZS,

        VAL,

        VEB,

        VEF,

        VND,

        VUV,

        WST,

        XAF,

        XAG,

        XAU,

        XCD,

        XDR,

        XEU,

        XOF,

        XPD,

        XPF,

        XPT,

        YER,

        YUN,

        ZAR,

        ZMK,

        ZWD

    }

    /// <summary>
    /// Enumération des différents taux d'intérêts disponibles
    /// </summary>
    public enum InterestRate
    {
        EURIBOR,
        USDEURIBOR,
        EONIA,
        EUREPO,
        EONIASWAP
    }

    /// <summary>
    /// Différents types de données que l'on peut récupérer
    /// </summary>
    enum TypeData
    {
        HistoricalData,
        Currency,
        InterestRate,
        RealTime
    }
    #endregion

    /// <summary>
    /// Classe abstraite correspondant à la base de donnée
    /// S'implémente de différente manières, suivant le type de données à récupérer et le type de récupération
    /// </summary>
    abstract public class Data
    {
        #region Attributs
        /// <summary>
        /// Base de donnée
        /// </summary>
        public DataSet Table { get; protected set; }

        /// <summary>
        /// Symbole des données traitées
        /// </summary>
        public List<string> Symbol { get; protected set; }

        /// <summary>
        /// Liste des colonnes
        /// </summary>
        public List<string> Columns { get; protected set; }

        /// <summary>
        /// Date de début de l'acquisition
        /// </summary>
        public DateTime Debut { get; protected set; }

        /// <summary>
        /// Date de fin de l'acquisition
        /// </summary>
        public DateTime Fin { get; protected set; }

        /// <summary>
        /// Type des données
        /// </summary>
        internal TypeData Type { get;  set; }

        #endregion


        public Data()
		{
			Table = new DataSet();
        }

        #region Méthodes
        /// <summary>
        /// Cette méthode permet de remplir la base de données. Son comportement dépend de l'attibut _Strategy.
        /// </summary>
        public void ImportData(Import i)
        {
            i.Import(this);

        }
        /// <summary>
        /// Cette méthode permet d'exporter les données selon un format dépendant du parametre
        /// </summary>
        /// <param name="e">précise le format d'exportation</param>
        public virtual void Export(Export e)
        {
            e.Export(this);
        }
        #endregion
    }
}