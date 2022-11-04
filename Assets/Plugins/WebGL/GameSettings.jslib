mergeInto(LibraryManager.library, 
{
	//ゲーム開始時コールバック
	CallBackStart:function()
	{
		var callback_start =function() 
		{
		    
		}
		
		game_start(callback_start);
	},
	//ゲーム終了時コールバック
	CallBackEnd:function(game_score)
	{
		var callback_end = function()
		{
			
		}
		console.log(game_score);
		game_end({callback: callback_end, score: game_score});
	},
	//ゲームリワード広告視聴完了
	CallBackShowReward:function()
	{
		var reward_success_callback = function()
		{

		}
		show_reward_ad(reward_success_callback);
	},
	//ゲームリワード広告視聴不完了
	CallBackFailShowReward:function()
	{
		var reward_fail_callback = function()
		{

		}
		show_reward_ad({reward_fail_callback:'fail_func'});
	},

});