package com.example.filip.quizdown;

import android.os.AsyncTask;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ExecutionException;


public class ApiHandler {

        //Logging tag
        private static final String TAG = ApiHandler.class.getSimpleName();

        public ApiHandler() {
        }

        //------------------GET--------------------------------------

        //get value of some attribute with id as value
        public String getAttributeWithValue(String stringUrl, String value, String attributeName){
            String result = "";
            List<String> resultList;

            result = getResponse(stringUrl, "GET", value);
            try{
                resultList = new ParseJSON().execute(result, attributeName).get();
                if(!resultList.isEmpty()) {
                    result = resultList.get(0);
                }
            }catch (InterruptedException e){
                Log.e(TAG, "InterruptedException: " + e.getMessage());
            }catch (ExecutionException e){
                Log.e(TAG, "ExecutionException: " + e.getMessage());
            }

            return  result;
        }

        //returns list of strings contains all values for specific attribute name
        public List<String> getAttributes(String stringUrl, String attributeName){
            List<String> resultList = new ArrayList<>();

            String result = getResponse(stringUrl, "GET");
            try{
                resultList = new ParseJSON().execute(result, attributeName).get();
            }catch (InterruptedException e){
                Log.e(TAG, "InterruptedException: " + e.getMessage());
            }catch (ExecutionException e){
                Log.e(TAG, "ExecutionException: " + e.getMessage());
            }

            return resultList;
        }

        //gets the whole JSON in string
        private String getResponse(String stringUrl, String method){
            String result = "";
            try{
                result = new ConnectToInternet().execute(stringUrl, method).get();
            }catch (InterruptedException e){
                Log.e(TAG, "InterruptedException: " + e.getMessage());
            }catch (ExecutionException e){
                Log.e(TAG, "ExecutionException: " + e.getMessage());
            }

            return result;
        }

        //gets the whole JSON in string for a specific value
        private String getResponse(String stringUrl, String method, String value){
            String result = "";
            stringUrl += "/" + value;
            try{
                result = new ConnectToInternet().execute(stringUrl, method).get();
            }catch (InterruptedException e){
                Log.e(TAG, "InterruptedException: " + e.getMessage());
            }catch (ExecutionException e){
                Log.e(TAG, "ExecutionException: " + e.getMessage());
            }

            return result;
        }

        //-----------------------POST-----------------------------

        //insert event, sends JSON to server with POST method
        /*public String insertEvent(String stringUrl, Events events){
            String json = "";

            try {
                JSONObject parentObject = new JSONObject();
                parentObject.accumulate("action", "create");


                JSONObject jsonObject = new JSONObject();
                jsonObject.accumulate("name", events.getName());
                jsonObject.accumulate("location", events.getLocation());
                jsonObject.accumulate("date", events.getDate());

                JSONArray array = new JSONArray();
                array.put(jsonObject);
                parentObject.accumulate("data", array);
                json = parentObject.toString();

            }catch (JSONException e){
                Log.e(TAG, "JSONException: " + e.getMessage());
            }

            String result = sendPost(stringUrl, json);

            return result;
        }*/

        //update event, sends JSON to server with POST method
        /*public String updateEvent(String stringUrl, Events events, int id){
            String json = "";

            try {
                JSONObject parentObject = new JSONObject();
                parentObject.accumulate("action", "update");


                JSONObject jsonObject = new JSONObject();
                jsonObject.accumulate("id", id);
                jsonObject.accumulate("name", events.getName());
                jsonObject.accumulate("location", events.getLocation());
                jsonObject.accumulate("date", events.getDate());

                JSONArray array = new JSONArray();
                array.put(jsonObject);
                parentObject.accumulate("data", array);
                json = parentObject.toString();

            }catch (JSONException e){
                Log.e(TAG, "JSONException: " + e.getMessage());
            }

            String result = sendPost(stringUrl, json);

            return result;
        }*/

        //Send post request to server
        private String sendPost(String url, String json){
            String result = "";
            try{
                result = new ConnectToInternet().execute(url, "POST", json).get();
            }catch (InterruptedException e){
                Log.e(TAG, "InterruptedException: " + e.getMessage());
            }catch (ExecutionException e){
                Log.e(TAG, "ExecutionException: " + e.getMessage());
            }
            return  result;
        }

        //converts InputStream to string
        private String convertStreamToString(InputStream inputStream){
            BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream));
            StringBuilder sb = new StringBuilder();

            String line;
            try {
                while ((line = reader.readLine()) != null) {
                    sb.append(line).append('\n');
                }
            } catch (IOException e) {
                Log.e(TAG, "IOException: " + e.getMessage());
            } finally {
                try {
                    inputStream.close();
                } catch (IOException e) {
                    Log.e(TAG, "IOException: " + e.getMessage());
                }
            }
            return sb.toString();
        }

        //--------------DELETE------------------
        public String deleteEvent(String url, int id){
            String result = "";
            url += "/" + String.valueOf(id);
            try{
                result = new ConnectToInternet().execute(url, "DELETE").get();
            }catch (InterruptedException e){
                Log.e(TAG, "InterruptedException: " + e.getMessage());
            }catch (ExecutionException e){
                Log.e(TAG, "ExecutionException: " + e.getMessage());
            }
            return  result;
        }

        //--------THREADS--------------------
        //Thread for connecting to internet
        private class ConnectToInternet extends AsyncTask<String, Void, String>{
            @Override
            protected String doInBackground(String... strings) {
                String result = "";
                try{
                    URL url = new URL(strings[0]);
                    HttpURLConnection connection = (HttpURLConnection) url.openConnection();
                    connection.setRequestMethod(strings[1]);
                    if(strings[1] == "POST" || strings[1] == "post"){
                        OutputStream os = connection.getOutputStream();
                        BufferedWriter writer = new BufferedWriter(
                                new OutputStreamWriter(os, "UTF-8"));
                        writer.write(strings[2]);

                        writer.flush();
                        writer.close();
                        os.close();
                    }
                    InputStream inputStream = new BufferedInputStream(connection.getInputStream());
                    result = convertStreamToString(inputStream);
                }
                catch (MalformedURLException e){
                    Log.e(TAG, "MalformedURLException: " + e.getMessage());
                }
                catch (IOException e){
                    Log.e(TAG, "MalformedURLException: " + e.getMessage());
                }
                return  result;
            }

        }


        //Thread for parsing JSON
        private class ParseJSON extends AsyncTask<String, Void, List<String>>{
            @Override
            protected List<String> doInBackground(String... strings) {
                List<String> stringList = new ArrayList<>();
                try {
                    JSONObject jsonObj;
                    JSONArray jsonArray = new JSONArray(strings[0]);
                    for(int i=0; i<jsonArray.length(); i++){
                        jsonObj = jsonArray.getJSONObject(i);
                        stringList.add(jsonObj.getString(strings[1]));
                    }
                }catch (JSONException e){
                    Log.e(TAG, "JSONException: " + e.getMessage());
                }
                return stringList;
            }
        }
    }
