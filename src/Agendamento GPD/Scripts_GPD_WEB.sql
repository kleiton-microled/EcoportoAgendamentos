ALTER TABLE sgipa.tb_cntr_bl ADD
("EMISSAO_DOC_SAIDA" DATE,
"SERIE_DOC_SAIDA" VARCHAR2(15),
"TIPO_DOC_SAIDA" VARCHAR2(10),
"NUM_DOC_SAIDA" NUMBER,
"NUM_PROTOCOLO" NUMBER,
"ANO_PROTOCOLO"  NUMBER(4),
"AUTONUM_MOTORISTA" NUMBER,
"AUTONUM_VEICULO" NUMBER
);

CREATE SEQUENCE SGIPA.SEQ_CNTR_BL_PROT_2012 START WITH 1
 INCREMENT BY   1
 NOCACHE
 NOCYCLE;

CREATE SEQUENCE SGIPA.SEQ_OS2012 START WITH 1
 INCREMENT BY   1
 NOCACHE
 NOCYCLE;

ALTER TABLE OPERADOR.TB_VIAGENS ADD ("DATA_LATE" DATE);
ALTER TABLE OPERADOR.TB_GD_CONTEINER ADD ("FLAG_LATE" NUMBER(1));
ALTER TABLE OPERADOR.TB_BOOKING ADD ("FLAG_LATE" NUMBER(1));
ALTER TABLE OPERADOR.TB_GD_CONTEINER RENAME COLUMN AUTONUM_GD_MOTORISTA_V TO AUTONUM_GD_MOTORISTA; 

ALTER TABLE OPERADOR.TB_GD_CONTEINER ADD ("AUTONUM_VEICULO" NUMBER);
ALTER TABLE OPERADOR.TB_AG_MOTORISTAS DROP CONSTRAINT PK_AG_MOTORISTAS;
ALTER TABLE OPERADOR.TB_AG_MOTORISTAS ADD CONSTRAINT PK_AG_MOTORISTA PRIMARY KEY (AUTONUM,CNH);
ALTER TABLE OPERADOR.TB_AG_MOTORISTAS ADD (CONSTRAINT PK_TB_AG_MOTORISTAS PRIMARY KEY (AUTONUM, CNH));
ALTER TABLE OPERADOR.TB_AG_MOTORISTAS DROP COLUMN ID_VEICULO;


CREATE OR REPLACE FORCE VIEW operador.vw_praca_aberta (reserva,
                                                       exportador,
                                                       navioviagem,
                                                       porto_descarga,
                                                       porto_destino,
                                                       data_dead_line,
                                                       autonum_viagem,
                                                       pod,
                                                       ef,
                                                       imo1,
                                                       imo2,
                                                       imo3,
                                                       imo4,
                                                       un1,
                                                       un2,
                                                       un3,
                                                       un4,
                                                       temperatura,
                                                       escala,
                                                       overheight,
                                                       overwidth,
                                                       overlength,
                                                       overwidthl,
                                                       obs,
                                                       flag_bloqueado,
                                                       carrier,
                                                       dt_inicio_praca,
                                                       dt_fim_praca,
                                                       autonum_booking,
                                                       flag_inicio_especial,
                                                       flag_fim_especial,
                                                       tipobasico,
                                                       quant20,
                                                       quant40,
                                                       data_late,
                                                       flag_late
                                                      )
AS
   SELECT b.REFERENCE AS reserva, p.razao AS exportador,
          (n.nome || ' ' || v.viagem) AS navioviagem,
          (b.pod || '-' || p1.nome) AS porto_descarga,
          (b.fdes || '-' || p2.nome) AS porto_destino, v.data_dead_line,
          v.autonum AS autonum_viagem, b.pod, b.ef, b.imo1, b.imo2, b.imo3,
          b.imo4, b.un1, b.un2, b.un3, b.un4, b.temperatura, b.escala,
          b.overheight, b.overwidth, b.overlength, b.overwidthl,
          DECODE (b.flag_special_care, 1, 'SPECIAL CARE', '') AS obs,
          NVL (p.flag_bloqueado, 0) AS flag_bloqueado,
          NVL (b.carrier, '') AS carrier,
          DECODE (b.dt_op_inicio,
                  NULL, v.data_op_inicio,
                  b.dt_op_inicio
                 ) AS dt_inicio_praca,
          DECODE (b.dt_op_fim,
                  NULL, v.data_dead_line,
                  b.dt_op_fim
                 ) AS dt_fim_praca,
          b.autonum AS autonum_booking,
          DECODE (b.dt_op_inicio, NULL, 0, 1) AS flag_inicio_especial,
          DECODE (b.dt_op_fim, NULL, 0, 1) AS flag_fim_especial, b.tipobasico,
          b.quant20, b.quant40, v.data_late, b.flag_late
     FROM operador.tb_booking b,
          operador.tb_viagens v,
          operador.tb_cad_clientes p,
          operador.tb_cad_navios n,
          operador.tb_cad_portos p1,
          operador.tb_cad_portos p2
    WHERE b.autonumviagem = v.autonum
      AND b.shipper = p.autonum
      AND v.navio = n.autonum
      AND b.pod = p1.codigo(+)
      AND b.fdes = p2.codigo(+)
      AND v.operando = 1
      AND b.atual = 1
      AND NVL (b.flag_transbordo, 0) = 0
      AND NVL (v.flag_interna, 0) = 0;
