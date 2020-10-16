<?php
   header("Access-Control-Allow-Origin: *");
   $file = fopen("Save1.txt", "r");
   echo fread($file, filesize("Save1.txt"));
   fclose($file);
?>