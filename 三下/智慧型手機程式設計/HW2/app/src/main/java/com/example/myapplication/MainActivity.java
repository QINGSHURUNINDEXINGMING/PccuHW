package com.example.myapplication;

import android.app.DatePickerDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class MainActivity extends AppCompatActivity {

    private Button chooseButton;
    private int year, month, day;

    private void processViews()
    {
        chooseButton=(Button)findViewById(R.id.chooseButton);
    }

    public void clickButton01(View view)          //選擇日期
    {






    }





    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);









    }
}
