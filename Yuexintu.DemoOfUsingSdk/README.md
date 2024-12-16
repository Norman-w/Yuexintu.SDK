### 可能遇到的问题

* 本地调试的时候可使用localhost:51648访问,但是如果部署在服务器通过nginx代理的时候不能访问
* 关键设置: `rewrite ^/face-capture-camera/(.*)$ /$1 break;` 但在其他项目中(Norman.Log.Server)未发现此问题不知何故
* 其他设置:
  ```nginx
  location /face-capture-camera/ {
    rewrite ^/face-capture-camera/(.*)$ /$1 break;
                proxy_pass http://192.168.6.6:51648;
                proxy_set_header Host $host;
                proxy_set_header X-Real-IP $remote_addr;
                proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
                proxy_set_header X-NginX-Proxy true;
                proxy_set_header X-Request-Uri $request_uri;
            }
  ```