.286
.MODEL SMALL
.STACK
.DATA
    NO1 DB 2
    NO2 DB 4
    NO3 DB 5
.CODE
    .STARTUP

    MOV AH,1
    INT 21h
    SUB AL,'0'
    MOV NO1,AL

    MOV AH,1
    INT 21h
    MOV NO2,AL
 
    MOV AH,1
    INT 21h
    SUB AL,'0'
    MOV NO3,AL

    MOV DL,NO2
    CMP DL,2Bh
    JE L1
    jmp L2
L1:
    MOV AL,NO1
    MOV DL,NO3

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
    jmp L3
L2:
    
    MOV AL,NO1
    MOV DL,NO3

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
L3:
     
    .EXIT

END