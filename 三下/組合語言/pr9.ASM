.MODEL SMALL
.STACK
.DATA
    NO1 DB 2
    NO2 DB 3
.CODE
    MOV AX,@DATA
    MOV DS,AX

    MOV AH,1
    INT 21h
    SUB AL,'0'
    MOV NO1,AL

    MOV AH,1
    INT 21h
    SUB AL,'0'
    MOV NO2,AL

    MOV AH, 1
    INT 21H
    
    MOV AL,NO1
    MOV DL,NO2

    cmp al,'+'
    JE  EQUALADD

    MUL DL
    mov ah,0h
    mov cl,0ah
    div cl
    add ax, 3030h
    mov bx, ax

    MOV DL,bl
    MOV AH,02
    INT 21H

    mov dl,bh
    mov ah,02
    int 21h

    MOV AH,4CH
    INT 21H


EQUALADD:
    ADD AL, DL
    mov ah,0h
    mov cl,0ah
    div cl
    add ax, 3030h
    mov bx, ax

    MOV DL,bl
    MOV AH,02
    INT 21H

    mov dl,bh
    mov ah,02
    int 21h

    MOV AH,4CH
    INT 21H

END

