<?php
	$servername = "blitzdbinstance.cihkvhti3s6i.sa-east-1.rds.amazonaws.com";
    $dbname = "dbblitz"; 
	$username = "avlgames";
	$password = "Larissa42";
	
	//Variables
	$cd_player		= $_POST["cdplayerPost"];
	$player_name	= $_POST["playerNamePost"]; 
	$gender         = $_POST["genderPost"]; 
	$age            = $_POST["agePost"]; 
	$education      = $_POST["educationPost"]; 
	$city           = $_POST["cityPost"]; 
	$play_games     = $_POST["playGamesPost"]; 
	


	
	$conn = new mysqli($servername, $username, $password, $dbname );

	if(!$conn){
		die("Connection Failed" . mysqli_connect_error());
	}


	$sql = "INSERT INTO player_info
						(cd_player,
						player_name,
						gender,
						age,
						education,
						city,
						play_games)
				VALUES('".$cd_player."',
					'".$player_name."',
					'".$gender."',
					'".$age."',
					'".$education."',
					'".$city."',
					'".$play_games."')";
					
	$result = mysqli_query($conn, $sql);

	if(!$result) echo "There was an error";
	else echo "Everything OK";  
	

?>