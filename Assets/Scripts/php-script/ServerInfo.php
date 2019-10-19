<?php
	$servername = "blitzdbinstance.cihkvhti3s6i.sa-east-1.rds.amazonaws.com";

    $dbname = "dbblitz"; 
	$username = "avlgames";
	$password = "Larissa42";
	
	$conn = new mysqli($servername, $username, $password, $dbname );

	if(!$conn){
		die("Connection Failed" . mysqli_connect_error());
	}


	$sql = "SELECT cd_player, player_name, age FROM player_info";
	$result = mysqli_query($conn, $sql);

	if(mysqli_num_rows($result) > 0){
		while($row = mysqli_fetch_assoc($result)){
			echo "cd_player:".$row['cd_player'] . "|player_name:".$row['player_name'];
		}
	}
	

?>