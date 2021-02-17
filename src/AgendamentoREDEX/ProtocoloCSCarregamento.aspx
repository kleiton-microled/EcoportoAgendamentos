<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProtocoloCSCarregamento.aspx.vb" Inherits="AgendamentoREDEX.ProtocoloCSCarregamento" %>

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
 
    </style>

</head>
<body>


<form id="Form1" runat="server">
    <div id="conteudo" runat="server"></div>
    <center>
    <div style="height:20px;line-height:20px; font-size:16px;margin-top:40px;">
            <p>Atenção: É necessário apresentar 03 vias do CTE e Danfe no Gate!</p>
        </div>
        </center> 
</form>

    
</body>
</html>
