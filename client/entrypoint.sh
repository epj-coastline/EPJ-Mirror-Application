#!/bin/sh

# Replace env vars in JavaScript files
echo "Replacing env vars in JS"
for file in app/js/*.js*;
do
  echo "Processing $file ...";

  # Use the existing JS file as template
  if [ ! -f $file.tmpl.js ]; then
    cp $file $file.tmpl.js
  fi

  envsubst '$COASTLINE_API_URI,$AUTH0_DOMAIN,$AUTH0_CLIENT_ID,$AUTH0_AUDIENCE,$AUTH0_REDIRECT_URI' < $file.tmpl.js > $file

done

echo "Starting Nginx"
nginx -g 'daemon off;'
