--没有电影ID但有影院ID时
--先获取电影名
select Top 1 mi.MovieName from MovieInfo mi,ChipInfo cp
				where mi.MovieName = cp.MovieName
				and cp.CinemaID = 1
				and datediff(MINUTE, getdate(),StartTime) > 15

--需要查询该电影院Top1的排片
select  cp.StartTime ,	cp.StopTime,  mi.MovieArea,	oi.OfficeID,	cp.ChipInfoID,	mi.MovieMoney ,	oi.OfficeName
from CinemaInfo ci,OfficeInfo oi,ChipInfo cp,MovieInfo mi
where ci.CinemaID = oi.CinemaID 
and cp.MovieName = mi.MovieName
and cp.CinemaID = ci.CinemaID
and cp.OfficeID = oi.OfficeID
and ci.CinemaID=1 
and mi.MovieName = (select Top 1 mi.MovieName from MovieInfo mi,ChipInfo cp
				where mi.MovieName = cp.MovieName
				and cp.CinemaID = 1
				and datediff(MINUTE, getdate(),StartTime) > 15 )
and datediff(MINUTE,getdate(),cp.StartTime)>15