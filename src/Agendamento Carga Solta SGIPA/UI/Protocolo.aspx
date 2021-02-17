<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Protocolo.aspx.vb" Inherits="AgendamentoCargaSoltaSGIPA.Protocolo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
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
        }
        
        thead
        {
            font-family:Arial;
            font-size:11px;
            font-weight: bold;
            background-color:<%= Session("SIS_COR_PADRAO") %>;
            color:White;
        }
        
         tbody
        {
            font-family:Courier New;
             font-size:11px;
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
            page-break-after: auto;
        }
        
        caption
        {
            font-family:Arial;
            font-size:12px;
            font-weight: bold;
            padding-top:8px;
            text-align:left;            
        }
        
        .assinatura
        {
            height:120px;
        }
        
        .assin
        {
            height:40px;
        }
 
    </style>
</head>
<body>
<center>
</center>
</body>
</html>
