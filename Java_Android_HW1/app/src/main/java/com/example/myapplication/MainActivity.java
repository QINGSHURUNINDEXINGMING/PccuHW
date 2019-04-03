package com.example.myapplication;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.method.ScrollingMovementMethod;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import java.util.Arrays;

public class MainActivity extends AppCompatActivity {

    RadioButton one, two;               //威力彩、大樂透按鈕
    EditText number;               //需要的組數
    EditText result;               //執行結果
    Button button;               //執行按鈕
    RadioGroup IWant;               //兩個按鈕的區塊

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        one=(RadioButton) findViewById(R.id.one);
        two=(RadioButton) findViewById(R.id.two);
        number=(EditText) findViewById(R.id.number);
        result=(EditText) findViewById(R.id.editText4);

        result.setMovementMethod(ScrollingMovementMethod.getInstance());               //滾動拉霸

        button = (Button) findViewById(R.id.button);
        IWant = (RadioGroup) findViewById(R.id.radioGroup);
    }

    public void send_out(View view)
    {
        int num=0;
        if (!number.getText().toString().matches(""))
            num=Integer.parseInt(number.getText().toString());
        String Writing="";
        if (num!=0)
        {
            Writing=genLotteryNum();
            result.setText(Writing);
        }else result.setText("沒有選擇");
    }

    private boolean Exist(int numArray[], int number)
    {
        boolean status = false;
        for (int i=0;i<numArray.length;i++)
        {
            if(numArray[i]==number)
            {
                status=true;
                break;
            }
        }
        return status;
    }

    private String twoDigits(int number)
    {
        int digit2=(number/10);
        int digit1=number%10;

        return Integer.toString(digit2)+Integer.toString(digit1);
    }

    private String genLotteryNum()
    {

        int number1, count, number2, biggestNum, times=0;
        boolean flag;
        String Res;

        if(!number.getText().toString().matches(""))               //讀取組數
            times=Integer.parseInt(number.getText().toString());

        if(IWant.getCheckedRadioButtonId()==R.id.one)               //判斷類型
        {
            biggestNum = 49;
            Res = "大樂透" + Integer.toString(times) + "組號碼:\r\n";
        }else
        {
            biggestNum = 38;
            Res = "威力彩" +Integer.toString(times) + "組號碼:\r\n";
            Res += "            第一區                第二區\r\n";
        }

        for (int i = 0; i < times; i++)               //產生數字
        {
            count = 0;
            int lotteryNum[]=new int[6];

            for (int j = 0; j < 6; j++) lotteryNum[j] = 0;

            do
            {
                number1 = (int) (Math.random() * biggestNum + 1);
                flag = Exist(lotteryNum,  number1);
                if (flag==false)
                {
                    lotteryNum[count] = number1;
                    count++;
                }

            } while (count<6);

            Arrays.sort(lotteryNum);               //排列數字

            for (int k = 0; k < 6; k++)	Res += twoDigits(lotteryNum[k]) + "    ";               //數字儲存自字串

            if (biggestNum == 49) Res += "\r\n";
            else
            {
                number2 = (int) (Math.random() * 8 + 1);
                Res += "      " + Integer.toString(number2) + "\r\n";
            }
        }
        return Res;
    }
}
