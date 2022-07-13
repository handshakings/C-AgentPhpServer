<?php

$host = "127.0.0.1";
$port = 444;
//first parameter ipv4 (AF_INET) or ipv6
$socket = socket_create(AF_INET, SOCK_STREAM,SOL_TCP) or die("Socket not created");
$result = socket_bind($socket,$host,$port) or die("Failed to bind");
$result = socket_listen($socket,3) or die("Failed to listen");

do{
    $accept = socket_accept($socket) or die("Failed to accept");

    $msg = "Connected";
    socket_write($accept,$msg,strlen($msg)) or die("Failed to write");
    
    $msg = socket_read($accept,1024) or die("Failed to read msg");
    echo trim($msg);
}while(true);


//socket_close($socket,$accept);
?>