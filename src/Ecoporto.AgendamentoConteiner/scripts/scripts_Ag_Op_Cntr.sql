CREATE SEQUENCE  OPERADOR.SEQ_AGENDAMENTO_CNTR  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 155 NOCACHE  NOORDER  NOCYCLE ;
CREATE SEQUENCE  OPERADOR.SEQ_AGENDAMENTO_CNTR_PROTOCOLO  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 200 NOCACHE  NOORDER  NOCYCLE ;
CREATE SEQUENCE  OPERADOR.SEQ_AG_CNTR_ITENS_DANFES  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 299 NOCACHE  NOORDER  NOCYCLE ;
CREATE SEQUENCE  OPERADOR.SEQ_PROTOCOLO_AG_GERAL  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1000 NOCACHE  NOORDER  NOCYCLE ;

CREATE TABLE OPERADOR.TB_AGENDAMENTO_CNTR 
(	
    AUTONUM NUMBER(*,0) NOT NULL PRIMARY KEY, 
    AUTONUM_MOTORISTA NUMBER(*,0) NOT NULL, 
    AUTONUM_VEICULO NUMBER(*,0) NOT NULL, 
    AUTONUM_TRANSPORTADORA NUMBER(*,0) NOT NULL, 
    AUTONUM_PERIODO NUMBER(*,0), 
    DATA_HORA DATE DEFAULT SYSDATE, 
    AUTONUM_USUARIO NUMBER(*,0) NOT NULL, 
    PROTOCOLO VARCHAR2(20 BYTE), 
    FLAG_IMPRESSO NUMBER(*,0) DEFAULT 0, 
    FLAG_CEGONHA NUMBER(1,0) DEFAULT 0, 
    AUTONUM_GATE NUMBER(12,0) DEFAULT 0 NOT NULL, 
    ANO_PROTOCOLO NUMBER(4,0) DEFAULT 0 NOT NULL, 
    DT_ENTRADA DATE, 
    CTE VARCHAR2(35 BYTE), 
    EMAIL_REGISTRO VARCHAR2(400 BYTE),
	TIPO_OPERACAO NUMBER(1,0)
);

ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR ADD CONSTRAINT FK_AG_CNTR_VEI FOREIGN KEY (AUTONUM_VEICULO) REFERENCES OPERADOR.TB_AG_VEICULOS(AUTONUM);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR ADD CONSTRAINT FK_AG_CNTR_TRA FOREIGN KEY (AUTONUM_TRANSPORTADORA) REFERENCES OPERADOR.TB_CAD_TRANSPORTADORAS(AUTONUM);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR ADD CONSTRAINT FK_AG_CNTR_PER FOREIGN KEY (AUTONUM_PERIODO) REFERENCES OPERADOR.TB_GD_RESERVA(AUTONUM_GD_RESERVA);
 

CREATE SEQUENCE  OPERADOR.SEQ_AGENDAMENTO_CNTR_ITEM_CNTR  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 155 NOCACHE  NOORDER  NOCYCLE ;	

CREATE TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR 
(	
    AUTONUM INT NOT NULL PRIMARY KEY , 
    AUTONUM_AGENDAMENTO INT NOT NULL,
    SIGLA VARCHAR2(12 BYTE),         
    TEMPERATURA VARCHAR2(5 BYTE), 
    ESCALA VARCHAR2(1 BYTE), 
    IMO1 VARCHAR2(4 BYTE), 
    IMO2 VARCHAR2(4 BYTE), 
    IMO3 VARCHAR2(4 BYTE), 
    IMO4 VARCHAR2(4 BYTE), 
    ONU1 VARCHAR2(4 BYTE), 
    ONU2 VARCHAR2(4 BYTE), 
    ONU3 VARCHAR2(4 BYTE), 
    ONU4 VARCHAR2(4 BYTE), 
    LACRE1 VARCHAR2(15 BYTE), 
    LACRE2 VARCHAR2(15 BYTE), 
    LACRE3 VARCHAR2(15 BYTE), 
    LACRE4 VARCHAR2(15 BYTE), 
    LACRE5 VARCHAR2(15 BYTE), 
    LACRE6 VARCHAR2(15 BYTE), 
    LACRE7 VARCHAR2(15 BYTE), 
    OBS VARCHAR2(250 BYTE), 
    OH NUMBER(3,0) DEFAULT 0, 
    OW NUMBER(3,0) DEFAULT 0, 
    OWL NUMBER(3,0) DEFAULT 0, 
    OL NUMBER(3,0) DEFAULT 0,     
    TAMANHO NUMBER(2,0) DEFAULT 0, 
    TIPOBASICO VARCHAR2(2 BYTE), 
    VOLUMES NUMBER(8,0), 
    UMIDADE VARCHAR2(8 BYTE), 
    VENTILACAO VARCHAR2(8 BYTE), 
    LACRE_SIF VARCHAR2(15 BYTE)
);

ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD AUTONUM_BOOKING INT NOT NULL;
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD TARA NUMBER(8,0);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD BRUTO NUMBER(8,0);    
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD DAT VARCHAR(25);    
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD EF VARCHAR(1);    
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD QTDE NUMBER(2);

ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD CONSTRAINT FK_AG_ITEM_CNTR FOREIGN KEY (AUTONUM_AGENDAMENTO) REFERENCES OPERADOR.TB_AGENDAMENTO_CNTR(AUTONUM);	 


CREATE TABLE OPERADOR.TB_AG_CNTR_ITENS_DANFES 
(	
   AUTONUM INT NOT NULL PRIMARY KEY, 
   AUTONUM_AGENDAMENTO_ITEM_CNTR INT NOT NULL, 
   DANFE VARCHAR2(44 BYTE), 
   CFOP VARCHAR2(4 BYTE)
);
 
ALTER TABLE OPERADOR.TB_AG_CNTR_ITENS_DANFES ADD CONSTRAINT FK_AG_CNTR_ITEM_CNTR FOREIGN KEY (AUTONUM_AGENDAMENTO_ITEM_CNTR) REFERENCES OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR(AUTONUM);

ALTER TABLE OPERADOR.TB_AG_CNTR_ITENS_DANFES ADD SEL VARCHAR2(1);

ALTER TABLE OPERADOR.TB_BOOKING ADD SHIPPER_OWNER NUMBER(1,0);


CREATE OR REPLACE  VIEW vw_agendamento_booking (autonum_booking,
                                                     autonumviagem,
                                                     reserva,
                                                     pod,
                                                     qtde,
                                                     shipperowner,
                                                     tipo,
                                                     ef,
                                                     tamanho,
                                                     remarks,
                                                     imo,
                                                     uno,
                                                     abertura,
                                                     fechamento,
                                                     navio,
                                                     autonum_viagem,
                                                     viagem,
                                                     HASH,
                                                     exportador,
                                                     saldo,
                                                     reeferdesligado,
                                                     bagagem,
                                                     latearrival,
                                                     deltahoras,
													 Comprimento,
													 LateralDireita,
													 LateralEsquerda,
													 Altura, 
                                                     Temp, 
                                                     Escala, 
                                                     Ventilacao, 
                                                     Umidade
                                                    )
AS
  SELECT b.autonum AS autonum_booking, b.autonumviagem,
         b.REFERENCE AS reserva, b.pod, b.quant20,
         b.shipper_owner AS shipperowner, b.tipobasico AS tipo,
         DECODE (b.ef, 'E', 1, 'F', 2) ef, '20' AS tamanho,
            DECODE (NVL (b.overlength, 0),
                    0, '',
                    'OL:' || b.overlength
                   )
         || DECODE (NVL (b.overwidth, 0), 0, '', ' OW:' || b.overwidth)
         || DECODE (NVL (b.overwidthl, 0), 0, '', ' OWL:' || b.overwidthl)
         || DECODE (NVL (b.overheight, 0), 0, '', ' OH:' || b.overheight)
         || DECODE (b.temperatura, NULL, '', ' TEMP:')
         || NVL (b.temperatura, '')
         || NVL (b.escala, '')
         || DECODE (b.imo1, NULL, '', ' IMO:')
         || NVL (b.imo1, '')
         || ' '
         || NVL (b.un1, '')
         || ' '
         || NVL (b.imo2, '')
         || ' '
         || NVL (b.un2, '')
         || ' '
         || NVL (b.imo3, '')
         || ' '
         || NVL (b.un3, '')
         || ' '
         || NVL (b.imo4, '')
         || ' '
         || NVL (b.un4, '') AS remarks,
         TRIM ((b.imo1 || ' ' || b.imo2 || ' ' || b.imo3 || ' ' || b.imo4)
              ) imo,
         TRIM ((b.un1 || ' ' || b.un2 || ' ' || b.un3 || ' ' || b.un4)) uno,
         DECODE (b.dt_op_inicio,
                 NULL, v.data_op_inicio,
                 b.dt_op_inicio
                ) AS abertura,
         DECODE (b.dt_op_fim,
                 NULL, v.data_op_fim,
                 b.dt_op_fim
                ) AS fechamento, n.nome AS navio, v.autonum AS autonum_viagem,
         v.viagem,
         ORA_HASH (TO_CHAR (b.autonum || 20 || b.tipobasico)) AS HASH,
         SUBSTR (c.razao, 0, 20) AS exportador,
           b.quant20
         - (CASE WHEN B.EF = 'E' THEN  
         (SELECT NVL(SUM(qtde),0)
              FROM operador.tb_agendamento_cntr_item_cntr
             WHERE autonum_booking = b.autonum AND tamanho = 20) ELSE 
             (SELECT COUNT (1)
              FROM operador.tb_agendamento_cntr_item_cntr
             WHERE autonum_booking = b.autonum AND tamanho = 20)
             END) Saldo,                          
         b.flag_desligado, b.flag_bagagem,
         CASE
           WHEN (b.dt_op_fim IS NOT NULL)
           AND (b.dt_op_fim > NVL (v.data_op_fim, SYSDATE))
             THEN 1
           ELSE 0
         END AS latearrival,
         CASE
           WHEN (b.dt_op_fim IS NOT NULL)
           AND (b.dt_op_fim > NVL (v.data_op_fim, SYSDATE))
             THEN 0
           ELSE 0 --36
         END AS deltahoras,		 
		NVL(b.overlength, 0) As Comprimento,
		NVL(b.overwidth, 0) As LateralDireita,
		NVL(b.overwidthl, 0) As LateralEsquerda,
		NVL(b.overheight, 0) As Altura,
        NVL(b.Temperatura, 0) Temp, 
        NVL(b.Escala,' ') Escala, 
        NVL(b.Ventilacao, 0) Ventilacao, 
        NVL(REPLACE(REPLACE(b.Umidade,'OFF',''),'%',''), 0) Umidade
    FROM operador.tb_booking b,
         operador.tb_viagens v,
         operador.tb_cad_navios n,
         operador.tb_cad_clientes c
   WHERE b.autonumviagem = v.autonum
     AND v.navio = n.autonum
     AND b.shipper = c.autonum
     AND v.operando = 1
     AND b.atual = 1
     AND b.flag_transbordo = 0
     AND NVL (v.flag_interna, 0) = 0
     AND b.quant20 > 0
     AND NVL (b.flag_cancelado_navis, 0) = 0
  UNION
  SELECT b.autonum AS autonum_booking, b.autonumviagem,
         b.REFERENCE AS reserva, b.pod, b.quant40,
         b.shipper_owner AS shipperowner, b.tipobasico AS tipo,
         DECODE (b.ef, 'E', 1, 'F', 2) ef, '40' AS tamanho,
            DECODE (NVL (b.overlength, 0),
                    0, '',
                    'OL:' || b.overlength
                   )
         || DECODE (NVL (b.overwidth, 0), 0, '', ' OW:' || b.overwidth)
         || DECODE (NVL (b.overwidthl, 0), 0, '', ' OWL:' || b.overwidthl)
         || DECODE (NVL (b.overheight, 0), 0, '', ' OH:' || b.overheight)
         || DECODE (b.temperatura, NULL, '', ' TEMP:')
         || NVL (b.temperatura, '')
         || NVL (b.escala, '')
         || DECODE (b.imo1, NULL, '', ' IMO:')
         || NVL (b.imo1, '')
         || ' '
         || NVL (b.un1, '')
         || ' '
         || NVL (b.imo2, '')
         || ' '
         || NVL (b.un2, '')
         || ' '
         || NVL (b.imo3, '')
         || ' '
         || NVL (b.un3, '')
         || ' '
         || NVL (b.imo4, '')
         || ' '
         || NVL (b.un4, '') AS remarks,
         TRIM ((b.imo1 || ' ' || b.imo2 || ' ' || b.imo3 || ' ' || b.imo4)
              ) imo,
         TRIM ((b.un1 || ' ' || b.un2 || ' ' || b.un3 || ' ' || b.un4)) uno,
         DECODE (b.dt_op_inicio,
                 NULL, v.data_op_inicio,
                 b.dt_op_inicio
                ) AS abertura,
         DECODE (b.dt_op_fim,
                 NULL, v.data_op_fim,
                 b.dt_op_fim
                ) AS fechamento, n.nome AS navio, v.autonum AS autonum_viagem,
         v.viagem,
         ORA_HASH (TO_CHAR (b.autonum || 40 || b.tipobasico)) AS HASH,
         SUBSTR (c.razao, 0, 20) AS exportador,
           b.quant40
         -  (CASE WHEN B.EF = 'E' THEN
         (SELECT NVL(SUM(qtde),0)
              FROM operador.tb_agendamento_cntr_item_cntr
             WHERE autonum_booking = b.autonum AND tamanho = 40) ELSE 
             (SELECT COUNT (1)
              FROM operador.tb_agendamento_cntr_item_cntr
             WHERE autonum_booking = b.autonum AND tamanho = 40)
             END) Saldo,     
         b.flag_desligado, b.flag_bagagem,
         CASE
           WHEN (b.dt_op_fim IS NOT NULL)
           AND (b.dt_op_fim > NVL (v.data_op_fim, SYSDATE))
             THEN 1
           ELSE 0
         END AS latearrival,
         CASE
           WHEN (b.dt_op_fim IS NOT NULL)
           AND (b.dt_op_fim > NVL (v.data_op_fim, SYSDATE))
             THEN 0
           ELSE 0 --36
         END AS deltahoras,
		 NVL(b.overlength, 0) As Comprimento,
		NVL(b.overwidth, 0) As LateralDireita,
		NVL(b.overwidthl, 0) As LateralEsquerda,
		NVL(b.overheight, 0) As Altura,
        NVL(b.Temperatura, 0) Temp, 
        NVL(b.Escala,' ') Escala, 
        NVL(b.Ventilacao, 0) Ventilacao, 
        NVL(REPLACE(REPLACE(b.Umidade,'OFF',''),'%',''), 0) Umidade
    FROM operador.tb_booking b,
         operador.tb_viagens v,
         operador.tb_cad_navios n,
         operador.tb_cad_clientes c
   WHERE b.autonumviagem = v.autonum
     AND v.navio = n.autonum
     AND b.shipper = c.autonum
     AND v.operando = 1
     AND b.atual = 1
     AND b.flag_transbordo = 0
     AND NVL (v.flag_interna, 0) = 0
     AND b.quant40 > 0
     AND NVL (b.flag_cancelado_navis, 0) = 0;
		
		
CREATE TABLE OPERADOR.TIPO_DOC_TRANSITO_DUE 
(
    AUTONUM NUMBER(9) NOT NULL PRIMARY KEY,
    DESCR VARCHAR2(10) NOT NULL
);

INSERT INTO OPERADOR.TIPO_DOC_TRANSITO_DUE (AUTONUM, DESCR) VALUES (1, 'DAT');
INSERT INTO OPERADOR.TIPO_DOC_TRANSITO_DUE (AUTONUM, DESCR) VALUES (2, 'MIC');
INSERT INTO OPERADOR.TIPO_DOC_TRANSITO_DUE (AUTONUM, DESCR) VALUES (3, 'TIF');
INSERT INTO OPERADOR.TIPO_DOC_TRANSITO_DUE (AUTONUM, DESCR) VALUES (4, 'DTAI');


ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD AUTONUM_TIPO_DOC_TRANSITO_DUE NUMBER(9);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD NR_DOC_TRANSITO_DUE  VARCHAR(15);		
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD DATA_DOC_TRANSITO_DUE DATE;

ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD REEFER_LIGADO NUMBER(1,0) DEFAULT 0;

ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD IMO VARCHAR2(50);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD ONU VARCHAR2(50);

ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR MODIFY OH NUMBER(8,2);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR MODIFY OW NUMBER(8,2);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR MODIFY OWL NUMBER(8,2);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR MODIFY OL NUMBER(8,2);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD LIQUIDO NUMBER(18,2);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD ISO VARCHAR(4);
ALTER TABLE OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR ADD FLAG_ACEITOU_REEFER NUMBER(1,0);