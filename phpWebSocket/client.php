<?php

if(isset($_POST['submit']) && $_POST['msg'] != "")
{
    $host = "127.0.0.1";
    $port = 444;
    //first parameter ipv4 (AF_INET) or ipv6
    $socket = socket_create(AF_INET, SOCK_STREAM,SOL_TCP) or die("Socket not created");
    socket_connect($socket,$host,$port) or die("Failed to connnect");
    $msg = "Hello Server";
    socket_write($socket,$msg,strlen($msg)) or die("Failed to write");
}


?>