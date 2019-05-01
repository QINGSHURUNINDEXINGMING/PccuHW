package com.example.myapplication;

import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.DatePickerDialog.OnDateSetListener;
import android.content.DialogInterface;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;

import java.util.Calendar;

public class MainActivity extends AppCompatActivity {

    private Button chooseDateButton;
    private Button chooseTypeOfExercise;
    private int year, month, day;

    private String chooseTypeOfExercise_items[] = {"慢走", "慢跑", "快走", "快跑"};

    private int chooseTypeOfExercise_choice = -1;

    private void processViews()
    {
        chooseDateButton = (Button)findViewById(R.id.chooseDateButton);
        chooseTypeOfExercise = (Button)findViewById(R.id.chooseTypeOfExercise);
    }

    public void ChooseDate(View view)          //選擇日期按鈕
    {
        OnDateSetListener listener = new OnDateSetListener()
        {
            @Override
            public void onDateSet(DatePicker view, int year, int month, int day)          // 設定日期監聽類別
            {
                MainActivity.this.year=year;
                MainActivity.this.month=month;
                MainActivity.this.day=day;

                chooseDateButton.setText(year+"/"+(month+1)+"/"+day);
            }
        };
        DatePickerDialog d =new DatePickerDialog(this, listener, year, month, day);          // 顯示日期對話框
        d.show();

    }

    public void ChooseExercise(View view)         //選擇運動類型按鈕
    {
        AlertDialog.Builder d = new AlertDialog.Builder(MainActivity.this);         //建立對話框物件

        d.setTitle("Select...");          //設定標題

        d.setSingleChoiceItems          //設定單選項目
                (chooseTypeOfExercise_items, chooseTypeOfExercise_choice,
                        new DialogInterface.OnClickListener()
                        {
                            public void onClick(DialogInterface dialog, int which)
                            {
                                chooseTypeOfExercise_choice = which;
                            }
                        }
                );
        d.setPositiveButton          //確認選擇
                ("OK", new DialogInterface.OnClickListener()
                    {
                        @Override
                        public void onClick(DialogInterface dialog, int which)
                        {
                            if(chooseTypeOfExercise_choice != -1)
                            {
                                chooseTypeOfExercise.setText(chooseTypeOfExercise_items[chooseTypeOfExercise_choice]);
                            }
                        }
                    }
                );

        d.setNegativeButton("Cancel", null);
        d.show();
    }






    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        processViews();

        //取得目前日期與時間
        Calendar cal = Calendar.getInstance();

        year= cal.get(Calendar.YEAR);
        month=cal.get(Calendar.MONTH);
        day=cal.get(Calendar.DAY_OF_MONTH);

    }
}
