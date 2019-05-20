.MODEL SMALL
.STACK
.DATA
     NO1 DB 9
     NO2 DB 5
.CODE
BEG: MOV AX,@DATA
     MOV DS,AX
     MOV AL,NO1
     MOV BL,NO2
     ADD AL,BL
     MOV AH,0
     AAA
     MOV BX,AX
     MOV DL,AH
     ADD DL,30H
     MOV AH,2
     INT 21H
     MOV DL,BL
     ADD DL,30H
     MOV AH,2
     INT 21H
     MOV AH,4CH
     INT 21H
     END BEG