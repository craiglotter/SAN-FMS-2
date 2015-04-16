<?php

function formatTime($sTime) {
	if (!is_numeric($sTime)) return false;
	
	$Year = (int) substr($sTime,0,4);
	$Month = (int) substr($sTime,4,2);
	$Day = (int) substr($sTime,6,2);
	$Hour = (int) substr($sTime,8,2);
	$Minute = (int) substr($sTime,10,2);
	
	if (!checkdate($Month, $Day, $Year)) return false;
	if ($Hour > 23) return false;
	if ($Minute > 59) return false;
	return mktime($Hour, $Minute, 0, $Month, $Day, $Year);
}

function explode_with_keys( $str, $separator = '|' ) {
		// Explode an associative array
		//
		// For historical reasons, accepts either
		// a string or an array
		$output = array();
		if ( is_array( $str ) ) {
			$array = $str;
		} else {
			$array = explode( $separator, $str );
		}
		
		for ( $i = 0; $i < count( $array ); $i = $i + 2 ) {
			$key = $array[ $i ];
			$output[ $key ] = $array[ $i+1 ];
		}
		
		return ( $output );
	} 


function sb_read_file( $filename ) {
		// Safely read a file.
		//
		// Returns either the contents of the file or NULL on fail.
		
		if ( version_compare( phpversion(), '4.3.0' ) == -1 ) {
			$result = NULL;
			if ( file_exists( $filename ) ) {
				$fp = @fopen( $filename, 'r' );
				if ( $fp ) {
					flock( $fp, LOCK_SH );
					$result = fread( $fp, filesize( $filename )*100 );
					if ( ( strpos( $filename, '.gz' ) !== false ) && ( extension_loaded( 'zlib' ) ) ) {
						$result = gzinflate( substr( $result, 10 ) );
					}
					flock( $fp, LOCK_UN );
					fclose( $fp );
				}
			}
		}
		else {
			$result=@file_get_contents( $filename );
			if ( ( strpos( $filename, '.gz' ) !== false ) && ( extension_loaded( 'zlib' ) ) ) {
				$result = gzinflate( substr( $result, 10 ) );
			}
		}
		
		return( $result );
	}
	

    function formatfilesize( $data ) {
    
        // bytes
        if( $data < 1024 ) {
        
            return $data . " bytes";
        
        }
        // kilobytes
        else if( $data < (1024 * 1024) ) {
        
            return round( ( $data / 1024 ), 2 ) . " KB";
        
        }
        // megabytes
        else if( $data < (1024 * 1024 * 1024) )
        {
        
            return round( ( $data / (1024 * 1024) ), 2 ) . " MB";
        
        }
        // gigabytes
        else {
     
       return round( ( $data / (1024 * 1024 * 1024) ), 2 ) . " GB";
        
        }
    
    }
    


   function url_exists($url) {
       $a_url = parse_url($url);
       if (!isset($a_url['port'])) $a_url['port'] = 80;
       $errno = 0;
       $errstr = '';
       $timeout = 30;
       if(isset($a_url['host']) && $a_url['host']!=gethostbyname($a_url['host'])){
           $fid = fsockopen($a_url['host'], $a_url['port'], $errno, $errstr, $timeout);
           if (!$fid) return false;
           $page = isset($a_url['path'])  ?$a_url['path']:'';
           $page .= isset($a_url['query'])?'?'.$a_url['query']:'';
           fputs($fid, 'HEAD '.$page.' HTTP/1.0'."\r\n".'Host: '.$a_url['host']."\r\n\r\n");
           $head = fread($fid, 4096);
           fclose($fid);
           return preg_match('#^HTTP/.*\s+[200|302]+\s#i', $head);
       } else {
           return false;
       }
   }


 function strtotitle($title)  
 {  

 $words = explode(' ', $title); 
 foreach ($words as $key => $word) 
 { 
  if ($key == 0 or !in_array($word,$smallwordsarray))
  { $words[$key] = ucwords($word);
  } 
  }
  $newtitle = implode(' ', $words); 
  return $newtitle; 
  }

 function UsageStat($dir)  
 {  
  if( is_dir( $dir ) )
  {
        foreach( scandir( $dir ) as $item )
        {
            if( !strcmp( $item, '.' ) || !strcmp( $item, '..' )  || !strcmp( $item, '_vti_cnf' )) 
                continue;        
            $counter = $counter + UsageStat( $dir . "/" . $item);
        }    
    }
    else
    {
       $counter = $counter + filesize( $dir );
    }
    if (!$counter)
    {
    $counter = 0;
    }
  return $counter; 
  }
  
  function MD5_Checksum($file_path)
  {
	$hash = md5_file($file_path);
	return strtoupper($hash);
  }

?>
