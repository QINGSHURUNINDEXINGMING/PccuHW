package com.example.a6409001_app_finalproject;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.Gravity;
import android.view.ViewGroup;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.SeekBar;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toolbar;

public class MainActivity extends AppCompatActivity implements CompoundButton.OnCheckedChangeListener, SeekBar.OnSeekBarChangeListener {

    int valB=0, valM=0, valD=0;

    EditText editB, editM, editD, price;
    SeekBar seekBarB, seekBarM, seekBarD;
    Switch discountSwitch;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        price = (EditText) findViewById(R.id.price);
        editB = (EditText) findViewById(R.id.breadVal);
        editM = (EditText) findViewById(R.id.meatVal);
        editD = (EditText) findViewById(R.id.drinkVal);

        discountSwitch = (Switch) findViewById(R.id.discountSwitch);
        discountSwitch.setOnCheckedChangeListener(this);

        seekBarB = (SeekBar)findViewById(R.id.breadSeekBar);
        seekBarB.setOnSeekBarChangeListener(this);
        seekBarM = (SeekBar)findViewById(R.id.meatSeekBar);
        seekBarM.setOnSeekBarChangeListener(this);
        seekBarD = (SeekBar)findViewById(R.id.drinkSeekBar);
        seekBarD.setOnSeekBarChangeListener(this);

    }

    @Override
    public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
        price.setText((buttonView.isChecked()) ? String.format("%.2f", ((valB*30+valM*90+valD*20)*0.9)):Integer.toString(valB*30+valM*90+valD*20));
    }

    @Override
    public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
        switch(seekBar.getId()) {
            case R.id.breadSeekBar:
                editB.setText(Integer.toString(progress));
                valB = progress;
                onCheckedChanged(discountSwitch, discountSwitch.isChecked());
                break;
            case R.id.meatSeekBar:
                editM.setText(Integer.toString(progress));
                valM = progress;
                onCheckedChanged(discountSwitch, discountSwitch.isChecked());
                break;
            case R.id.drinkSeekBar:
                editD.setText(Integer.toString(progress));
                valD = progress;
                onCheckedChanged(discountSwitch, discountSwitch.isChecked());
                break;
        }
    }

    @Override
    public void onStartTrackingTouch(SeekBar seekBar) {

    }

    @Override
    public void onStopTrackingTouch(SeekBar seekBar) {

    }
}

