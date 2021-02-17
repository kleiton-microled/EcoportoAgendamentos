CREATE SEQUENCE OPERADOR.SEQ_TRANSPORTE_DEPOT  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 155 NOCACHE  NOORDER  NOCYCLE ;

CREATE TABLE TB_TRANSPORTE_DEPOT
(
    AUTONUM NUMBER(8) NOT NULL  PRIMARY KEY,
    TRANSPORTADORA NUMBER(8) NOT NULL,
    RECINTO NUMBER(8) NOT NULL ,
    FLAG_ATIVO  NUMBER(1)
);