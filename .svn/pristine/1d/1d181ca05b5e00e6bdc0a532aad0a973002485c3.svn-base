
/*----------------------------------------------Menu  (snehith) 07-09-2023--------------------------------------------------------------------*/
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `adm_mst_spGetMenuData`(user_gid varchar(100))
BEGIN

  SET @ExecuteSQL = CONCAT( 'SELECT a.module_gid, b.module_name, b.sref, b.icon, ', 'b.menu_level, b.module_gid_parent, b.display_order ', 'FROM adm_mst_tprivilege a ', 'LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid ', 'WHERE user_gid = ''', user_gid, ''' AND ', 'b.lw_flag = ''Y'' ', 'GROUP BY a.module_gid ', 'ORDER BY b.display_order ASC' );

    PREPARE stmt FROM @ExecuteSQL;

    EXECUTE stmt;

    DEALLOCATE PREPARE stmt;

    END$$
DELIMITER ;
/*--------------------------------------End:(snehith) ------------------------------------------------------*/
