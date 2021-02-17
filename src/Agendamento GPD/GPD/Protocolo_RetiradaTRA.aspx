<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Protocolo_RetiradaTRA.aspx.vb" Inherits="GPD.Protocolo_RetiradaTRA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Protocolo de Retirada</title>

        <style type="text/css">
        *
        {
            padding: 0;
            margin: 0;
        }
        
        table
        {            
            border-collapse: collapse;
            width: 800px;
            text-align: center;            
        }
        
        td
        {
            border: 1px solid black;
            height:18px;
        }
        
        thead
        {
            font-family:Arial;
            font-size:11px;
            font-weight: bold;
            color:White;
        }
          
         caption
        {
            font-family:Arial;
            font-size:12px;
            font-weight: bold;
            padding-top:8px;
            text-align:left;            
        }
        
         tbody
        {
            font-family:Arial;
            font-size:12px;
            font-weight:bold;
        }
        
         #cabecalho
        {
            margin-top:20px;
        }
        
        #cabecalho td
        {
            border:0px;
            font-family: Arial;
            font-weight: bold;
            font-size: 14px;
        }
        
        .itens td
        {
            border: 0px solid black;
        }
         
         .folha
        {
            page-break-after: always;
        }
 
 
         .assinatura
        {
            height:100px;
        }
 
        .style1
        {
            width: 100%;
        }
        
        .folha
        {
            page-break-after: always;
        }
 
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="conteudo" runat="server">
    </div>
    </form>
</body>
</html>
