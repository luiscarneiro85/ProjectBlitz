<?php
	$servername = "blitzdbinstance.cihkvhti3s6i.sa-east-1.rds.amazonaws.com";

    $dbname = "dbblitz"; 
	$username = "avlgames";
	$password = "Larissa42";
	
	//Variables
	$cd_player				= $_POST["cdplayerPost"];
	$intOfShoots            = $_POST["intOfShootsPost"];
	$intOfHits              = $_POST["intOfHitsPost"];
	$intOfBoxes             = $_POST["intOfBoxesPost"];
	$hpLost                 = $_POST["hpLostPost"];
	$heal                   = $_POST["healPost"];
	$seconds                = $_POST["secondsPost"];
	$distance               = $_POST["distancePost"];
	$firstSkill             = $_POST["firstSkillPost"];
	$mostUsedSkill          = $_POST["mostUsedSkillPost"];
	$distanceOfEnemys       = $_POST["distanceOfEnemysPost"];
	$distanceInRoom         = $_POST["distanceInRoomPost"];
	$distanceAux            = $_POST["distanceAuxPost"];
	


	
	$conn = new mysqli($servername, $username, $password, $dbname );

	if(!$conn){
		die("Connection Failed" . mysqli_connect_error());
	}


	$sql = "INSERT INTO knn_data
							(cd_player,
							intOfShoots,
							intOfHits,
							intOfBoxes,
							hpLost,
							heal,
							seconds,
							distance,
							firstSkill,
							mostUsedSkill,
							distanceOfEnemys,
							distanceInRoom,
							distanceAux)
							VALUES
							(	'".$cd_player."',
								CAST('".$intOfShoots."' AS FLOAT),
								CAST('".$intOfHits."' AS FLOAT),
								CAST('".$intOfBoxes."' AS FLOAT),
								CAST('".$hpLost."' AS FLOAT),
								CAST('".$heal."' AS FLOAT),
								CAST('".$seconds."' AS FLOAT),
								CAST('".$distance."' AS FLOAT),
								CAST('".$firstSkill."' AS FLOAT),
								CAST('".$mostUsedSkill."' AS FLOAT),
								CAST('".$distanceOfEnemys."' AS FLOAT),
								CAST('".$distanceInRoom."' AS FLOAT),
								CAST('".$distanceAux."' AS FLOAT))";
					
	$result = mysqli_query($conn, $sql);

	if(!$result) echo "There was an error";
	else echo "Everything OK";  
	

?>