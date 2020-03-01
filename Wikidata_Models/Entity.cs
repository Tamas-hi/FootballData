using System;
using System.Collections.Generic;
using System.Text;

namespace FootballData.Wikidata_Models
{
    public class En
    {
        public string language { get; set; }
        public string value { get; set; }
    }

    public class Labels
    {
        public En en { get; set; }
    }

    public class Value
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue
    {
        public Value value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class P641
    {
        public Mainsnak mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Value2
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue2
    {
        public Value2 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak2
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue2 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class P31
    {
        public Mainsnak2 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Value3
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue3
    {
        public Value3 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak3
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue3 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value4
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue4
    {
        public Value4 value { get; set; }
        public string type { get; set; }
    }

    public class P155
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue4 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Datavalue5
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class P1545
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue5 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value5
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue6
    {
        public Value5 value { get; set; }
        public string type { get; set; }
    }

    public class P156
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue6 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Qualifiers
    {
        public List<P155> P155 { get; set; }
        public List<P1545> P1545 { get; set; }
        public List<P156> P156 { get; set; }
    }

    public class P3450
    {
        public Mainsnak3 mainsnak { get; set; }
        public string type { get; set; }
        public Qualifiers qualifiers { get; set; }
        public List<string> __invalid_name__qualifiersorder { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Value6
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue7
    {
        public Value6 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak4
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue7 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class P17
    {
        public Mainsnak4 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Value7
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue8
    {
        public Value7 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak5
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue8 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value8
    {
        public string amount { get; set; }
        public string unit { get; set; }
    }

    public class Datavalue9
    {
        public Value8 value { get; set; }
        public string type { get; set; }
    }

    public class P1352
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue9 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value9
    {
        public string amount { get; set; }
        public string unit { get; set; }
    }

    public class Datavalue10
    {
        public Value9 value { get; set; }
        public string type { get; set; }
    }

    public class P1359
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue10 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value10
    {
        public string amount { get; set; }
        public string unit { get; set; }
    }

    public class Datavalue11
    {
        public Value10 value { get; set; }
        public string type { get; set; }
    }

    public class P1350
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue11 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value11
    {
        public string amount { get; set; }
        public string unit { get; set; }
    }

    public class Datavalue12
    {
        public Value11 value { get; set; }
        public string type { get; set; }
    }

    public class P1351
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue12 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value12
    {
        public string amount { get; set; }
        public string unit { get; set; }
    }

    public class Datavalue13
    {
        public Value12 value { get; set; }
        public string type { get; set; }
    }

    public class P1356
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue13 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value13
    {
        public string amount { get; set; }
        public string unit { get; set; }
    }

    public class Datavalue14
    {
        public Value13 value { get; set; }
        public string type { get; set; }
    }

    public class P1357
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue14 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value14
    {
        public string amount { get; set; }
        public string unit { get; set; }
    }

    public class Datavalue15
    {
        public Value14 value { get; set; }
        public string type { get; set; }
    }

    public class P1355
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue15 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Qualifiers2
    {
        public List<P1352> P1352 { get; set; }
        public List<P1359> P1359 { get; set; }
        public List<P1350> P1350 { get; set; }
        public List<P1351> P1351 { get; set; }
        public List<P1356> P1356 { get; set; }
        public List<P1357> P1357 { get; set; }
        public List<P1355> P1355 { get; set; }
    }

    public class P1923
    {
        public Mainsnak5 mainsnak { get; set; }
        public string type { get; set; }
        public Qualifiers2 qualifiers { get; set; }
        public List<string> __invalid_name__qualifiersorder { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Value15
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue16
    {
        public Value15 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak6
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue16 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class P2348
    {
        public Mainsnak6 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Value16
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue17
    {
        public Value16 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak7
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue17 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class P1346
    {
        public Mainsnak7 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Value17
    {
        public string amount { get; set; }
        public string unit { get; set; }
    }

    public class Datavalue18
    {
        public Value17 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak8
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue18 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class P1132
    {
        public Mainsnak8 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Value18
    {
        public string time { get; set; }
        public int timezone { get; set; }
        public int before { get; set; }
        public int after { get; set; }
        public int precision { get; set; }
        public string calendarmodel { get; set; }
    }

    public class Datavalue19
    {
        public Value18 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak9
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue19 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value19
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue20
    {
        public Value19 value { get; set; }
        public string type { get; set; }
    }

    public class P143
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue20 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Datavalue21
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class P4656
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue21 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Snaks
    {
        public List<P143> P143 { get; set; }
        public List<P4656> P4656 { get; set; }
    }

    public class Reference
    {
        public string hash { get; set; }
        public Snaks snaks { get; set; }
        public List<string> __invalid_name__snaksorder { get; set; }
    }

    public class P582
    {
        public Mainsnak9 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
        public List<Reference> references { get; set; }
    }

    public class Value20
    {
        public string time { get; set; }
        public int timezone { get; set; }
        public int before { get; set; }
        public int after { get; set; }
        public int precision { get; set; }
        public string calendarmodel { get; set; }
    }

    public class Datavalue22
    {
        public Value20 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak10
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue22 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value21
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue23
    {
        public Value21 value { get; set; }
        public string type { get; set; }
    }

    public class P1432
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue23 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Datavalue24
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class P46562
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue24 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Snaks2
    {
        public List<P1432> P143 { get; set; }
        public List<P46562> P4656 { get; set; }
    }

    public class Reference2
    {
        public string hash { get; set; }
        public Snaks2 snaks { get; set; }
        public List<string> __invalid_name__snaksorder { get; set; }
    }

    public class P580
    {
        public Mainsnak10 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
        public List<Reference2> references { get; set; }
    }

    public class Value22
    {
        public string amount { get; set; }
        public string unit { get; set; }
    }

    public class Datavalue25
    {
        public Value22 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak11
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue25 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value23
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue26
    {
        public Value23 value { get; set; }
        public string type { get; set; }
    }

    public class P1433
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue26 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Datavalue27
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class P46563
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue27 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Snaks3
    {
        public List<P1433> P143 { get; set; }
        public List<P46563> P4656 { get; set; }
    }

    public class Reference3
    {
        public string hash { get; set; }
        public Snaks3 snaks { get; set; }
        public List<string> __invalid_name__snaksorder { get; set; }
    }

    public class P13502
    {
        public Mainsnak11 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
        public List<Reference3> references { get; set; }
    }

    public class Value24
    {
        public string amount { get; set; }
        public string unit { get; set; }
    }

    public class Datavalue28
    {
        public Value24 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak12
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue28 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value25
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue29
    {
        public Value25 value { get; set; }
        public string type { get; set; }
    }

    public class P1434
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue29 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Datavalue30
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class P46564
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue30 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Snaks4
    {
        public List<P1434> P143 { get; set; }
        public List<P46564> P4656 { get; set; }
    }

    public class Reference4
    {
        public string hash { get; set; }
        public Snaks4 snaks { get; set; }
        public List<string> __invalid_name__snaksorder { get; set; }
    }

    public class P13512
    {
        public Mainsnak12 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
        public List<Reference4> references { get; set; }
    }

    public class Value26
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue31
    {
        public Value26 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak13
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue31 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value27
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue32
    {
        public Value27 value { get; set; }
        public string type { get; set; }
    }

    public class P1435
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue32 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Datavalue33
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class P46565
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue33 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Snaks5
    {
        public List<P1435> P143 { get; set; }
        public List<P46565> P4656 { get; set; }
    }

    public class Reference5
    {
        public string hash { get; set; }
        public Snaks5 snaks { get; set; }
        public List<string> __invalid_name__snaksorder { get; set; }
    }

    public class P664
    {
        public Mainsnak13 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
        public List<Reference5> references { get; set; }
    }

    public class Datavalue34
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak14
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue34 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Value28
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue35
    {
        public Value28 value { get; set; }
        public string type { get; set; }
    }

    public class P1436
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue35 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class Snaks6
    {
        public List<P1436> P143 { get; set; }
    }

    public class Reference6
    {
        public string hash { get; set; }
        public Snaks6 snaks { get; set; }
        public List<string> __invalid_name__snaksorder { get; set; }
    }

    public class P393
    {
        public Mainsnak14 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
        public List<Reference6> references { get; set; }
    }

    public class Value29
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue36
    {
        public Value29 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak15
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue36 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class P2500
    {
        public Mainsnak15 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Value30
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue37
    {
        public Value30 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak16
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue37 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class P2882
    {
        public Mainsnak16 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Value31
    {
        public string __invalid_name__entitytype { get; set; }
        public int __invalid_name__numericid { get; set; }
        public string id { get; set; }
    }

    public class Datavalue38
    {
        public Value31 value { get; set; }
        public string type { get; set; }
    }

    public class Mainsnak17
    {
        public string snaktype { get; set; }
        public string property { get; set; }
        public string hash { get; set; }
        public Datavalue38 datavalue { get; set; }
        public string datatype { get; set; }
    }

    public class P710
    {
        public Mainsnak17 mainsnak { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string rank { get; set; }
    }

    public class Claims
    {
        public List<P641> P641 { get; set; }
        public List<P31> P31 { get; set; }
        public List<P3450> P3450 { get; set; }
        public List<P17> P17 { get; set; }
        public List<P1923> P1923 { get; set; }
        public List<P2348> P2348 { get; set; }
        public List<P1346> P1346 { get; set; }
        public List<P1132> P1132 { get; set; }
        public List<P582> P582 { get; set; }
        public List<P580> P580 { get; set; }
        public List<P13502> P1350 { get; set; }
        public List<P13512> P1351 { get; set; }
        public List<P664> P664 { get; set; }
        public List<P393> P393 { get; set; }
        public List<P2500> P2500 { get; set; }
        public List<P2882> P2882 { get; set; }
        public List<P710> P710 { get; set; }
    }

    public class Q39052816
    {
        public string type { get; set; }
        public string id { get; set; }
        public Labels labels { get; set; }
        public Claims claims { get; set; }
    }

    public class Entities
    {
        public Q39052816 Q39052816 { get; set; }
    }

    public class RootEntityObject
    {
        public Entities entities { get; set; }
        public int success { get; set; }
    }
}
