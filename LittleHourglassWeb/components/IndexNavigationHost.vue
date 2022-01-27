<template>
	<div class="navigationAll">
		<div class="navigation_01">
			
			<vs-navbar class="vs_navbar" v-model="active"  square>
				
			  <template #left>
			    <img class="images" src="../static/icon.png" alt="">
			  </template>
				
			  <vs-navbar-item :active="active == 'guide'" id="guide">
			    标题01
			  </vs-navbar-item>
			  
			  <vs-navbar-item :active="active == 'guide02'" id="guide02">
			    标题02
			  </vs-navbar-item>
			  
			  <vs-navbar-item :active="active == 'guide03'" id="guide03">
			    标题03
			  </vs-navbar-item>
			  
			  <vs-navbar-item :active="active == 'guide04'" id="guide04">
			    标题04
			  </vs-navbar-item>
			  
			  <vs-navbar-item :active="active == 'guide05'" id="guide05">
			    标题05
			  </vs-navbar-item>		
					
			  <vs-navbar-item :active="active == 'guide06'" id="guide06">
			    标题06
			  </vs-navbar-item>		
				  
			  <vs-navbar-item :active="active == 'guide07'" id="guide07">
			    标题07
			  </vs-navbar-item>
			
			  <template>
			    <div class="center content-inputs">		  
			      <vs-input type="search" icon-after v-model="value2" placeholder="搜索一下" width="300px" >
			        <template #icon>
					  <img class="" src="../static/1x/search.png" alt="">
			        </template>
			      </vs-input>
			    </div>
			  </template>
			  
			<template #right>
	
				<vs-button border :active="active01 == 0" @click="active = 0" >
				  登录
				</vs-button>
				
				<vs-button border :active="active01 == 1" @click="active = 1" >
				  退出
				</vs-button>
		
			    <!-- <vs-button flat >登录</vs-button>
			    <vs-button>退出</vs-button> -->
			</template>
			  
			</vs-navbar>

			<!-- <ul class="navigation_ul">
				<li>标题导航1</li>
				<li>标题导航2</li>
				<li>标题导航3</li>
				<li>标题导航4</li>
				<li>标题导航4</li>
				<li>标题导航5</li>
				<li>标题导航6</li>
			</ul> -->
		</div>
		<div class="navigation_02">
			<!-- <img class="images_02" src="../static/backgroundImage/panorama-2488556_1920.jpg" alt=""> -->
		</div>
	</div>
</template>

<script>
	 export default {
	    data:() => ({
	      active: 'guide',
		  value2:"",
		  active01:""
		  // active01: 0
	    })
	  }
</script>

<style>
	.navigationAll {
		width: 100%;
		min-width: 1680px;
		min-height: 247px;
		border: 1px solid #000;
	}

	.navigation_01 {
		width: 100%;
		min-width: 1680px;
		min-height: 45px;
		border: 1px solid #F00;
		margin: 0 auto;
	}
	
	.vs_navbar{
		min-width: 1680px;
	}
	
	.images{
		height: 40px;
		width: 40px;
	}
	
	.navigation_02 {
		width: 100%;
		min-width: 1680px;
		min-height: 200px;
		border: 1px solid #00f;
		margin: 0 auto;
		background-color: rgb(255, 219, 217);
	}
	
	.images_02{
		width: 1000px;
		height: 200px;
	}
	
</style>
