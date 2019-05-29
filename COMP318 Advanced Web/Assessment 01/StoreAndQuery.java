import org.apache.jena.query.*;
import org.apache.jena.tdb.TDBFactory;
import org.apache.jena.util.FileManager;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;


class Main {
    public static void main(String[] args){
        StoreAndQuery storeAndQuery = new StoreAndQuery();
        storeAndQuery.queryAndOutputToCommandLine(StoreAndQuery.QuestionQuery.QUESTION_A);
        storeAndQuery.queryAndOutputToCommandLine(StoreAndQuery.QuestionQuery.QUESTION_B_QUERY01);
        storeAndQuery.queryAndOutputToCommandLine(StoreAndQuery.QuestionQuery.QUESTION_B_QUERY02);
        storeAndQuery.queryAndOutputToCommandLine(StoreAndQuery.QuestionQuery.QUESTION_B_QUERY03);
        storeAndQuery.queryAndOutputToCommandLine(StoreAndQuery.QuestionQuery.QUESTION_B_QUERY04);
        storeAndQuery.endUsage();
    }
}

public class StoreAndQuery {

   private static String DATASET_PATH = "NobelDB";
   private static String LOCAL_FILE_PATH = "nobelprize.nt";
   private static String ONLINE_FILE_PATH = "https://raw.githubusercontent.com/aooXu/Lecture/master/COMP313%20Advanced%20Web/nobelprize.nt";
   private static Map<String, String> PREFIX_SET = new HashMap<String, String>() {{
	        put("xmlns", "http://xmlns.com/foaf/0.1/");
	        put("xmlschema", "http://www.w3.org/2001/XMLSchema#");
	        put("dbpedia", "http://dbpedia.org/ontology/");
	        put("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
	        put("nobelprize", "http://data.nobelprize.org/terms/");
	        put("nobelprizecategory", "http://data.nobelprize.org/resource/category/");
	        put("nobelprizecountry", "http://data.nobelprize.org/resource/country/");

   }};

   public static enum QuestionQuery {

       QUESTION_A(
           "Question A",
           "SELECT * WHERE{ \n " +
               "\t ?subject ?predicate ?object. \n" +
           "} LIMIT 20"
       ),
       QUESTION_B_QUERY01(
           "Question B Query 01",
           "SELECT ?name ?category ?prizedYear WHERE{ \n" +
               "\t ?laureate xmlns:name ?name. \n" +
               "\t ?laureate xmlns:gender \"female\". \n" +
               "\t ?laureate nobelprize:nobelPrize ?nobelprize. \n" +
               "\t ?nobelprize nobelprize:category ?category. \n" +
               "\t ?laureate nobelprize:laureateAward ?laureateaward. \n" +
               "\t ?laureateaward nobelprize:year ?prizedYear. \n" +
               "\t FILTER(?prizedYear>2000) \n" +
           "}"
       ),
       QUESTION_B_QUERY02(
           "Question B Query 02",
           "SELECT ?name WHERE{ \n" +
               "\t ?laureate xmlns:name ?name. \n" +
               "\t ?laureate nobelprize:nobelPrize ?nobelprize. \n" +
               "\t ?nobelprize nobelprize:category nobelprizecategory:Peace. \n" +
               "\t ?laureate nobelprize:laureateAward ?laureateaward. \n" +
               "\t ?laureateaward nobelprize:year 1991. \n" +
           "}"
       ),
       QUESTION_B_QUERY03(
           "Question B Query 03",
           "SELECT ?name ?prizedAge ?birthday WHERE{ \n" +
               "\t ?laureate xmlns:name ?name. \n" +
               "\t ?laureate xmlns:birthday ?birthday. \n" +
               "\t ?laureate nobelprize:nobelPrize ?nobelprize. \n" +
               "\t ?nobelprize nobelprize:category nobelprizecategory:Chemistry . \n" +
               "\t ?laureate nobelprize:laureateAward ?laureateaward. \n" +
               "\t ?laureateaward nobelprize:year 2010. \n" +
               "\t BIND ((2010-year(?birthday)) as ?prizedAge) \n" +
           "}"
       ),
       QUESTION_B_QUERY04(
           "Question B Query 04",
           "SELECT ?name ?discipline ?givenName WHERE{ \n" +
               "\t ?laureate xmlns:name ?name. \n" +
               "\t ?laureate xmlns:givenName ?givenName. \n" +
               "\t ?laureate dbpedia:birthPlace nobelprizecountry:USA. \n" +
               "\t ?laureate nobelprize:nobelPrize ?nobelprize. \n" +
               "\t ?nobelprize nobelprize:category ?discipline. \n" +
               "\t FILTER REGEX(?givenName, \"Ernest\", \"i\") \n" +
           "}"
       );

       public String queryName;
       public String queryString;

       QuestionQuery(String queryName, String queryString){
           this.queryName = queryName;
           this.queryString = queryString;
       }
   }


   private Dataset nobelPrizeDataset;

   public StoreAndQuery() {
       nobelPrizeDataset = TDBFactory.createDataset(DATASET_PATH);
       if (!nobelPrizeDataset.isEmpty()) return;
       System.out.println("Please wait a second the program is downloading data from network.\n");
       InputStream nobelFileInputStream;
       try {
    	   URL url = new URL(ONLINE_FILE_PATH);   
    	   HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();   
    	   nobelFileInputStream = urlConnection.getInputStream(); 
       } catch (IOException ioE) {
           System.out.println("Network error, program is loading local file.\n");
           nobelFileInputStream = FileManager.get().open(LOCAL_FILE_PATH);
       }
       nobelPrizeDataset.getDefaultModel().read(nobelFileInputStream, null, "NT");
   }

   public void endUsage() {
       nobelPrizeDataset.end();
       nobelPrizeDataset.getDefaultModel().removeAll();
       nobelPrizeDataset.close();
   }

   public void queryAndOutputToCommandLine(QuestionQuery questionQuery) {
       System.out.println(questionQuery.queryName);
       String queryStr = questionQuery.queryString;
       StringBuilder prefixStrBuilder = new StringBuilder();
       PREFIX_SET.forEach((abbr,url)->{
           if(queryStr.contains(abbr+":"))
               prefixStrBuilder.append(String.format("PREFIX %s: <%s>\n", abbr, url));
       });
       System.out.println(prefixStrBuilder.toString() + queryStr);
       Query query = QueryFactory.create(prefixStrBuilder.toString() + queryStr);
       try (QueryExecution qexec = QueryExecutionFactory.create(query, nobelPrizeDataset)) {
           ResultSet results = qexec.execSelect();
           ResultSetFormatter.out(System.out, results, query);
       } catch (Exception ignored) {
           ignored.printStackTrace();
           System.err.println(ignored);
       }
   }
}


